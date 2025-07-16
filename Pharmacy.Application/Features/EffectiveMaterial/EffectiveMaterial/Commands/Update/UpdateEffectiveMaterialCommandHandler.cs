using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Update;

public class UpdateEffectiveMaterialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateEffectiveMaterialCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial> _effectiveMaterialRepository = unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial>();


    public override async Task<Result<string>> Handle(UpdateEffectiveMaterialCommand request, CancellationToken cancellationToken)
    {
        var effectiveMaterial = await _effectiveMaterialRepository.FindAsync(
            em => em.Id == request.Id,
            Include: em => em.Include(em => em.CommonUses)
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
                            .Include(em => em.EM_DrugInteractions)
                            .Include(em => em.MedicinesDrugInteractions)
                            .Include(em => em.MedicinesCrossSelling));


        if (effectiveMaterial == null)
            return Result<string>.Fail(Messages.EffectiveMaterialNotFound);

        mapper.Map(request, effectiveMaterial);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
