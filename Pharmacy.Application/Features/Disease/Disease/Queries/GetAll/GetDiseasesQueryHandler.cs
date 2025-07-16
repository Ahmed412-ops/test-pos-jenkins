using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Queries.GetAll;

public class GetDiseasesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetDiseasesQuery, Result<PaginationResponse<GetDiseasesResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Disease.Disease> _diseaseRepo = unitOfWork.GetRepository<Domain.Entities.Disease.Disease>();

    public override async Task<Result<PaginationResponse<GetDiseasesResponse>>> Handle(
        GetDiseasesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _diseaseRepo.GetAllQueryableAsync(d => !d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetDiseasesResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetDiseasesResponse>>.Success(
            new PaginationResponse<GetDiseasesResponse> { Data = response, Count = count }
        );
    }
}

