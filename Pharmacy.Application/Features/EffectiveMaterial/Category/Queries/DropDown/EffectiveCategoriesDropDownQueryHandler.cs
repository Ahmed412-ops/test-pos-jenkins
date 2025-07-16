using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.DropDown;

public class EffectiveCategoriesDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<EffectiveCategoriesDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<EffectiveMaterialCategory> _effectiveMaterialCategoryRepo = unitOfWork.GetRepository<EffectiveMaterialCategory>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        EffectiveCategoriesDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var effectiveMaterialCategories = await _effectiveMaterialCategoryRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(effectiveMaterialCategories);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
