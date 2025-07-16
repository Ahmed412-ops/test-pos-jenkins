using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Queries.DropDown;

public class SideEffectsDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<SideEffectsDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.SideEffects.SideEffect> _sideEffectRepo = unitOfWork.GetRepository<Domain.Entities.SideEffects.SideEffect>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        SideEffectsDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var sideEffects = await _sideEffectRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(sideEffects);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}

