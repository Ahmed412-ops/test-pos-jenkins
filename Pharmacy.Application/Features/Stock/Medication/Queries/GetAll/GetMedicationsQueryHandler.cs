using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetAll;

public class GetMedicationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : BaseHandler<GetMedicationsQuery, Result<PaginationResponse<GetMedicationsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationStock> _medicationRepo = unitOfWork.GetRepository<Domain.Entities.Stock.MedicationStock>();
    public override async Task<Result<PaginationResponse<GetMedicationsResponse>>> Handle(
        GetMedicationsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _medicationRepo.GetAllQueryableAsync(d=>!d.Is_Deleted, 
            Include: q => q.Include(a => a.Medicine));

        if(!string.IsNullOrWhiteSpace(request.MedicineName))
            query = query.Where(a => a.Medicine.Name.Contains(request.MedicineName));

        if(!string.IsNullOrWhiteSpace(request.Barcode))
            query = query.Where(a => a.GeneratedBarcode.Contains(request.Barcode));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetMedicationsResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetMedicationsResponse>>.Success(
            new PaginationResponse<GetMedicationsResponse> { Data = response, Count = count }
        );
    }
}
