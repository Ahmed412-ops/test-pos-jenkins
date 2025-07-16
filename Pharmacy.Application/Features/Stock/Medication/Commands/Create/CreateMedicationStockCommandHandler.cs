using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.GeneratorService;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.Create;

public class CreateMedicationStockCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IGenerateLocalBarCode generateLocalBarCode,
    IStockHistoryService stockHistoryService,
    ICurrentUser currentUser
) : BaseHandler<CreateMedicationStockCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationStockRepository =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();
    private readonly IGenericRepository<Domain.Entities.Medicine.Medicine> _medicineRepository =
        unitOfWork.GetRepository<Domain.Entities.Medicine.Medicine>();

    public override async Task<Result<string>> Handle(
        CreateMedicationStockCommand request,
        CancellationToken cancellationToken
    )
    {
        var medicine = await _medicineRepository.FindAsync(x => x.Id == request.MedicineId);
        if (medicine == null)
            return Result<string>.Fail(Messages.MedicineNotFound);

        var localBarcode = generateLocalBarCode.GenerateLocalBarcode(
            request.ExpiryDate,
            request.SellingPrice,
            medicine.Index
        );
        var existingMedicationStock = await _medicationStockRepository.FindAsync(x =>
            x.GeneratedBarcode == localBarcode
        );

        if (existingMedicationStock != null)
        {
            existingMedicationStock.Quantity += request.Quantity;
        }
        else
        {
            var medicationStock = mapper.Map<Domain.Entities.Stock.MedicationStock>(request);
            medicationStock.GeneratedBarcode = localBarcode;
            await _medicationStockRepository.AddAsync(medicationStock);
        }

        var updatedStockLevel = await stockHistoryService.GetOverallStockLevelAsync(
            request.MedicineId,
            request.Quantity
        );

        var logDto = new StockHistoryLogDto
        {
            MedicineId = request.MedicineId,
            TransactionDate = DateTime.Now,
            TransactionType = StockTransactionType.Added,
            QuantityChange = request.Quantity,
            UpdatedStockLevel = updatedStockLevel,
            PerformedById = currentUser.GetUserId(),
            TransactionReference = localBarcode,
            ReasonForChange = null,
        };

        await stockHistoryService.LogTransactionAsync(logDto);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(message: Messages.SuccessfullyCreated, data: localBarcode);
    }
}
