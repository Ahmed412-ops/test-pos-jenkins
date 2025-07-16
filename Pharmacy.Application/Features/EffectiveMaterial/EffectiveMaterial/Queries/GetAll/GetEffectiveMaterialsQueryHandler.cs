using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetAll;

public class GetEffectiveMaterialsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<GetEffectiveMaterialsQuery, Result<PaginationResponse<GetEffectiveMaterialsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial> _effectiveMaterialRepo = unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial>();


    public override async Task<Result<PaginationResponse<GetEffectiveMaterialsResponse>>> Handle(
        GetEffectiveMaterialsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _effectiveMaterialRepo.GetAllQueryableAsync(d => !d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetEffectiveMaterialsResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetEffectiveMaterialsResponse>>.Success(
            new PaginationResponse<GetEffectiveMaterialsResponse> { Data = response, Count = count }
        );
    }
}
