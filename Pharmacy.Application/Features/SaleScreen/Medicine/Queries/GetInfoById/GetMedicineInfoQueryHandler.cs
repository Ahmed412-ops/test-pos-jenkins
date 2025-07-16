using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetInfoById;

public class GetMedicineInfoQueryHandler(IUnitOfWork unitOfWork)
    : BaseHandler<GetMedicineInfoQuery, Result<GetMedicineInfoResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationStockRepo =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();

    public override async Task<Result<GetMedicineInfoResponse>> Handle(
        GetMedicineInfoQuery request,
        CancellationToken cancellationToken
    )
    {
        var medicationStock = await _medicationStockRepo.FindAsync(
            m => m.Id == request.Id,
            select: MedicineDtoMapper.MedicineInfoProjection(),
            asSplit: true,
            asNoTracking: true
        );

        if (medicationStock == null)
            return Result<GetMedicineInfoResponse>.Fail(Messages.MedicineNotFound);

        if (!await IsLatestToExpire(medicationStock.MedicineId, medicationStock.Id))
            return Result<GetMedicineInfoResponse>.Success(
                data: medicationStock,
                message: Messages.MedicineNotLatestToExpire
            );
        if (medicationStock.IsUsedForChildren
            && request.Weight.HasValue
            && medicationStock.DosagePerKgForChildren.HasValue)
            medicationStock.CalculatedDose = medicationStock.DosagePerKgForChildren.Value * request.Weight.Value;

        return Result<GetMedicineInfoResponse>.Success(data: medicationStock);
    }

    private async Task<bool> IsLatestToExpire(Guid MedicineId, Guid StockId)
    {
        var medicationStock = await _medicationStockRepo.FindAsync(
            d =>
                d.MedicineId == MedicineId
                && d.Quantity > 0
                && d.ExpiryDate >= DateOnly.FromDateTime(DateTime.Now),
            orderBy: q => q.OrderBy(a => a.ExpiryDate)
        );

        if (medicationStock == null || medicationStock.Id != StockId)
            return false;

        return true;
    }
}
