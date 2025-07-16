using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.GeneratorService;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.Update;

public class UpdateMedicationStockCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IGenerateLocalBarCode generateLocalBarCode,
    IStockHistoryService stockHistoryService,
    ICurrentUser currentUser
) : BaseHandler<UpdateMedicationStockCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationStockRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();
    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepository =
        unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();

    public override async Task<Result<string>> Handle(
        UpdateMedicationStockCommand request,
        CancellationToken cancellationToken
    )
    {
        var oldMedicationStock = await _medicationStockRepository.FindAsync(x =>
            x.Id == request.Id
        );
        if (oldMedicationStock == null)
            return Result<string>.Fail(Messages.NotFound);

        var medicine = await _medicineRepository.FindAsync(x => x.Id == request.MedicineId);
        if (medicine == null)
            return Result<string>.Fail(Messages.MedicineNotFound);

        var newBarcode = generateLocalBarCode.GenerateLocalBarcode(
            request.ExpiryDate,
            request.SellingPrice,
            medicine.Index
        );

        var existingMedicationStock = await _medicationStockRepository.FindAsync(x =>
            x.GeneratedBarcode == newBarcode && x.Id != request.Id
        );

        decimal delta = 0; // The amount of change.
        if (existingMedicationStock != null)
        {
            existingMedicationStock.Quantity += request.Quantity;
            delta = request.Quantity;
            // Mark the old record as deleted.
            oldMedicationStock.Is_Deleted = true;
        }
        else
        {
            delta = request.Quantity - oldMedicationStock.Quantity;
            mapper.Map(request, oldMedicationStock);
            oldMedicationStock.GeneratedBarcode = newBarcode;
        }

        var updatedStockLevel = await stockHistoryService.GetOverallStockLevelAsync(
            request.MedicineId,
            delta
        );

        var logDto = new StockHistoryLogDto
        {
            MedicineId = request.MedicineId,
            TransactionType = StockTransactionType.Adjusted,
            QuantityChange = delta,
            UpdatedStockLevel = updatedStockLevel,
            PerformedById = currentUser.GetUserId(),
            TransactionReference = newBarcode,
            ReasonForChange = null, 
        };

        await stockHistoryService.LogTransactionAsync(logDto);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated, newBarcode);
    }
}
