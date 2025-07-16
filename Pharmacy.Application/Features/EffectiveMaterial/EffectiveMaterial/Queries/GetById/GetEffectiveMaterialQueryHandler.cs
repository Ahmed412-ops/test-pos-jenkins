using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Queries.GetById;

public class GetEffectiveMaterialQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<GetEffectiveMaterialQuery, Result<GetEffectiveMaterialResponse>>
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial> _effectiveMaterialRepo = unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial>();

    public override async Task<Result<GetEffectiveMaterialResponse>> Handle(GetEffectiveMaterialQuery request, CancellationToken cancellationToken)
    {
        var effectiveMaterial = await _effectiveMaterialRepo.FindAsync(
            em => em.Id == request.Id,
            Include: em => em.Include(em => em.Category)
                            .Include(em => em.CommonUses)
                            .ThenInclude(m => m.Use!)
                            .Include(em => em.OffLabelUses)
                            .ThenInclude(o => o.Use!)
                            .Include(em => em.SideEffects)
                            .ThenInclude(s => s.SideEffect!)
                            .Include(em => em.FoodInteractions)
                            .ThenInclude(f => f.Food!)
                            .Include(em => em.DiseaseInteraction)
                            .ThenInclude(d => d.Disease!)
                            .Include(em => em.EM_CrossSelling)
                            .ThenInclude(cs => cs.CrossSellingMaterial)
                            .Include(em => em.EM_DrugInteractions)
                            .ThenInclude(di => di.InteractingMaterial!)
                            .Include(em => em.MedicinesDrugInteractions)
                            .ThenInclude(mdi => mdi.Medicine!)
                            .Include(em => em.MedicinesCrossSelling)
                            .ThenInclude(mcs => mcs.Medicine!));

        if (effectiveMaterial == null)
            return Result<GetEffectiveMaterialResponse>.Fail(Messages.EffectiveMaterialNotFound);

        var response = mapper.Map<GetEffectiveMaterialResponse>(effectiveMaterial);

        return Result<GetEffectiveMaterialResponse>.Success(response);
    }
}
