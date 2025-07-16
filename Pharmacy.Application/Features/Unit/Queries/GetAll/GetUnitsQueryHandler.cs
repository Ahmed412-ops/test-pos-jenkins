using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Queries.GetAll;

public class GetUnitsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetUnitsQuery, Result<PaginationResponse<GetUnitsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Unit.Unit> _unitRepo = unitOfWork.GetRepository<Domain.Entities.Unit.Unit>();

    public override async Task<Result<PaginationResponse<GetUnitsResponse>>> Handle(
        GetUnitsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _unitRepo.GetAllQueryableAsync(d=>!d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetUnitsResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetUnitsResponse>>.Success(
            new PaginationResponse<GetUnitsResponse> { Data = response, Count = count }
        );
    }
}
