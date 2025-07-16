using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Return.Queries.GetById;

public class GetMedicationReturnQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetMedicationReturnQuery, Result<GetMedicationReturnResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationReturn> _returnRepo =
        unitOfWork.GetRepository<Domain.Entities.Stock.MedicationReturn>();

    public override async Task<Result<GetMedicationReturnResponse>> Handle(
        GetMedicationReturnQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _returnRepo.GetAllQueryableAsync(
            d => !d.Is_Deleted,
            Include: a =>
                a.Include(b => b.Supplier)
                    .Include(b => b.SupplierInvoice!)
                    .Include(b => b.ReturnItems)
                    .ThenInclude(m => m.MedicineUnit)
                    .ThenInclude(mu => mu.Medicine)
                    .Include(b => b.ReturnItems)
                    .ThenInclude(m => m.MedicineUnit)
                    .ThenInclude(mu => mu.Unit),
            asNoTracking: true
        );

        var medicationReturn = query.FirstOrDefault(a => a.Id == request.Id);

        if (medicationReturn == null)
            return Result<GetMedicationReturnResponse>.Fail(Messages.NotFound);

        var response = mapper.Map<GetMedicationReturnResponse>(medicationReturn);

        return Result<GetMedicationReturnResponse>.Success(response);
    }
}
