using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Return.Commands.SetStatus;

public class SetStatusCommandHandler(
    IUnitOfWork unitOfWork,
    IStockHistoryService stockHistoryService,
    ICurrentUser currentUser
) : BaseHandler<SetStatusCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationReturn> _returnRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationReturn>();
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _stockRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();

    public override async Task<Result<bool>> Handle(
        SetStatusCommand request,
        CancellationToken cancellationToken
    )
    {
        var medicationReturn = await _returnRepository.FindAsync(
            f => f.Id == request.Id,
            Include: f => f.Include(i => i.ReturnItems)
        );

        if (medicationReturn == null)
            return Result<bool>.Fail(Messages.NotFound);

        if (medicationReturn.ReturnStatus == request.ReturnStatus)
            return Result<bool>.Fail(Messages.NoChangesDetected);

        if (medicationReturn.ReturnStatus == ReturnStatus.Refunded)
            return Result<bool>.Fail(Messages.ReturnAlreadyCompleted);

        if (request.ReturnStatus == ReturnStatus.Refunded)
        {
            var logTasks = new List<Task>();
            foreach (var item in medicationReturn.ReturnItems)
            {
                var stock = await _stockRepository.FindAsync(f =>
                    f.GeneratedBarcode == item.Barcode
                );
                if (stock == null)
                    return Result<bool>.Fail(Messages.MedicationStockNotFound);

                stock.Quantity -= item.QuantityToReturn;
                var overallStockLevel = await stockHistoryService.GetOverallStockLevelAsync(
                    stock.MedicineId,
                    -item.QuantityToReturn
                );

                var logDto = new StockHistoryLogDto
                {
                    MedicineId = stock.MedicineId,
                    TransactionType = StockTransactionType.Returned,
                    QuantityChange = -item.QuantityToReturn,
                    UpdatedStockLevel = overallStockLevel,
                    PerformedById = currentUser.GetUserId(),
                    TransactionReference = medicationReturn.ReturnReferenceNumber,
                    ReasonForChange = null,
                };

                logTasks.Add(stockHistoryService.LogTransactionAsync(logDto));
            }
            await Task.WhenAll(logTasks);
        }

        medicationReturn.ReturnStatus = request.ReturnStatus;
        await unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true, Messages.SuccessfullyUpdated);
    }
}
