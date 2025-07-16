using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Return.Queries.GetAll;

public class GetMedicationReturnsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetMedicationReturnsQuery, Result<PaginationResponse<GetMedicationReturnsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Stock.MedicationReturn> _returnRepo = unitOfWork.GetRepository<Domain.Entities.Stock.MedicationReturn>();
    public override async Task<Result<PaginationResponse<GetMedicationReturnsResponse>>> Handle(
        GetMedicationReturnsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _returnRepo.GetAllQueryableAsync(d=>!d.Is_Deleted,
                                     Include: a => a.Include(b => b.Supplier)
                                                   .Include(b => b.SupplierInvoice!));

        if(!string.IsNullOrWhiteSpace(request.ReturnReferenceNumber))
            query = query.Where(a => a.ReturnReferenceNumber == request.ReturnReferenceNumber);
        
        if(!string.IsNullOrWhiteSpace(request.SupplierName))
            query = query.Where(a => a.Supplier.Name == request.SupplierName);
        
        if (request.ReturnStartDate.HasValue)
            query = query.Where(a => a.ReturnDate >= request.ReturnStartDate);

        if (request.ReturnEndDate.HasValue)
            query = query.Where(a => a.ReturnDate <= request.ReturnEndDate);
        
        if (request.ReturnStatus.HasValue)
            query = query.Where(a => a.ReturnStatus == request.ReturnStatus);

        var count = await query.CountAsync(cancellationToken);
        var response = query
            .Select(a => mapper.Map<GetMedicationReturnsResponse>(a))
            .Paginate(request)
            .ToList();
        
        return Result<PaginationResponse<GetMedicationReturnsResponse>>.Success(
            new PaginationResponse<GetMedicationReturnsResponse>{Data = response, Count = count});
    }
}
