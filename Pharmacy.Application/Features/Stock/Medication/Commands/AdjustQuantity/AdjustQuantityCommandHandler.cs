using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.AdjustQuantity;

public class AdjustQuantityCommandHandler(
    IUnitOfWork unitOfWork,
    IStockHistoryService stockHistoryService,
    ICurrentUser currentUser
) : BaseHandler<AdjustQuantityCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();

    public override async Task<Result<string>> Handle(
        AdjustQuantityCommand request,
        CancellationToken cancellationToken
    )
    {
        var medication = await _medicationRepository.FindAsync(s => s.Id == request.Id);
        if (medication == null)
            return Result<string>.Fail(Messages.MedicationStockNotFound);

        var previousQuantity = medication.Quantity;
        medication.Quantity = request.NewQuantity;
        
        var delta = previousQuantity - request.NewQuantity;
        var overallStockLevel = await stockHistoryService.GetOverallStockLevelAsync(medication.MedicineId, delta);

        var logDto = new StockHistoryLogDto
        {
            MedicineId = medication.MedicineId,
            TransactionType = StockTransactionType.Adjusted,
            QuantityChange = delta,
            UpdatedStockLevel = overallStockLevel,
            PerformedById = currentUser.GetUserId(),
            TransactionReference = medication.GeneratedBarcode,
            ReasonForChange = null,
        };

        await stockHistoryService.LogTransactionAsync(logDto);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
