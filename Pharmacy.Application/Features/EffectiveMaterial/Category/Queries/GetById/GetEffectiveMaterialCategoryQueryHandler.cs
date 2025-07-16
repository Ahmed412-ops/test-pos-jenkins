using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetById;

public class GetEffectiveMaterialCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<GetEffectiveMaterialCategoryQuery, Result<GetEffectiveMaterialCategoryResponse>>
{
    private readonly IGenericRepository<EffectiveMaterialCategory> _effectiveMaterialCategoryRepo = unitOfWork.GetRepository<EffectiveMaterialCategory>();


    public override async Task<Result<GetEffectiveMaterialCategoryResponse>> Handle(GetEffectiveMaterialCategoryQuery request, CancellationToken cancellationToken)
    {
        var effectiveMaterialCategory = await _effectiveMaterialCategoryRepo.FindAsync(
            d => d.Id == request.Id,
            Include: d=> d.Include(dc => dc.EffectiveMaterials),
            asNoTracking: true);

        if (effectiveMaterialCategory == null)
            return Result<GetEffectiveMaterialCategoryResponse>.Fail(Messages.EffectiveMaterialCategoryNotFound);

        var response = mapper.Map<GetEffectiveMaterialCategoryResponse>(effectiveMaterialCategory);

        return Result<GetEffectiveMaterialCategoryResponse>.Success(response);
    }
}
