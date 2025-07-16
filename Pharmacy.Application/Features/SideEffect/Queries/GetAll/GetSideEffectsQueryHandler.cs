using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Queries.GetAll;

public class GetSideEffectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
  : BaseHandler<GetSideEffectsQuery, Result<PaginationResponse<GetSideEffectsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.SideEffects.SideEffect> _sideEffectRepo = unitOfWork.GetRepository<Domain.Entities.SideEffects.SideEffect>();

    public override async Task<Result<PaginationResponse<GetSideEffectsResponse>>> Handle(
        GetSideEffectsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _sideEffectRepo.GetAllQueryableAsync(d=>!d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetSideEffectsResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetSideEffectsResponse>>.Success(
            new PaginationResponse<GetSideEffectsResponse> { Data = response, Count = count }
        );
    }
}
