using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.DropDown;

public class EffectiveMaterialsDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<EffectiveMaterialsDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial> _effectiveMaterialRepo = unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        EffectiveMaterialsDropDownQuery request,
        CancellationToken cancellationToken
    )
    {
        var effectiveMaterials = await _effectiveMaterialRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(effectiveMaterials);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
