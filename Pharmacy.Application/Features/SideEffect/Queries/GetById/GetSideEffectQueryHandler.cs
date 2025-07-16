using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Queries.GetById;

public class GetSideEffectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetSideEffectQuery, Result<GetSideEffectResponse>>
{
    private readonly IGenericRepository<Domain.Entities.SideEffects.SideEffect> _sideEffectRepo = unitOfWork.GetRepository<Domain.Entities.SideEffects.SideEffect>();

    public override async Task<Result<GetSideEffectResponse>> Handle(
        GetSideEffectQuery request,
        CancellationToken cancellationToken
    )
    {
        var sideEffect = await _sideEffectRepo.FindAsync(se => se.Id == request.Id,
        Include: se => se.Include(se => se.EffectiveMaterialSideEffects)
                        .ThenInclude(em => em.EffectiveMaterial!));

        if (sideEffect == null)
            return Result<GetSideEffectResponse>.Fail(Messages.SideEffectNotFound);

        var response = mapper.Map<GetSideEffectResponse>(sideEffect);
        return Result<GetSideEffectResponse>.Success(response);
    }
}
