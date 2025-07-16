using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Return.Commands.Create;

public class CreateMedicationReturnCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IStockHistoryService stockHistoryService,
    ICurrentUser currentUser
) : BaseHandler<CreateMedicationReturnCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationReturn> _returnStockRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationReturn>();
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _stockRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();

    public override async Task<Result<string>> Handle(
        CreateMedicationReturnCommand request,
        CancellationToken cancellationToken
    )
    {
        var returnStock = mapper.Map<Domain.Entities.Stock.MedicationReturn>(request);
        await _returnStockRepository.AddAsync(returnStock);

        if (request.ReturnStatus == ReturnStatus.Refunded)
        {
            var tasks = new List<Task>();
            foreach (var item in request.ReturnItems)
            {
                var stock = await _stockRepository.FindAsync(f =>
                    f.GeneratedBarcode == item.Barcode
                );
                if (stock == null)
                    return Result<string>.Fail(Messages.MedicationStockNotFound);

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
                    TransactionReference = returnStock.ReturnReferenceNumber,
                    ReasonForChange = item.AdditionalReasonDetails,
                };

                tasks.Add(stockHistoryService.LogTransactionAsync(logDto));
            }
            await Task.WhenAll(tasks);
        }

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(
            message: Messages.SuccessfullyCreated,
            data: returnStock.ReturnReferenceNumber
        );
    }
}
