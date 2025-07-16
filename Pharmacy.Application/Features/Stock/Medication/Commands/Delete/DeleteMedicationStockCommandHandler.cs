using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.Delete;

public class DeleteMedicationStockCommandHandler(
    IUnitOfWork unitOfWork,
    IStockHistoryService stockHistoryService,
    ICurrentUser currentUser
) : BaseHandler<DeleteMedicationStockCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationStockRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();

    public override async Task<Result<bool>> Handle(
        DeleteMedicationStockCommand request,
        CancellationToken cancellationToken
    )
    {
        var medicationStock = await _medicationStockRepository.FindAsync(f => f.Id == request.Id);
        if (medicationStock == null)
            return Result<bool>.Fail(Messages.NotFound);

        medicationStock.Is_Deleted = true;

        var quantityToRemove = medicationStock.Quantity;
        var updatedStockLevel = await stockHistoryService.GetOverallStockLevelAsync(
            medicationStock.MedicineId,
            -quantityToRemove
        );

        var logDto = new StockHistoryLogDto
        {
            MedicineId = medicationStock.MedicineId,

            TransactionType = StockTransactionType.Deleted,
            QuantityChange = -quantityToRemove,
            UpdatedStockLevel = updatedStockLevel, 
            PerformedById = currentUser.GetUserId(),
            TransactionReference = medicationStock.GeneratedBarcode, // or another appropriate reference
            ReasonForChange = "Stock deleted",
        };

        await stockHistoryService.LogTransactionAsync(logDto);

        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
