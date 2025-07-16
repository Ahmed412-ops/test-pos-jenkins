using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Delete;

public class DeleteEffectiveMaterialCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteEffectiveMaterialCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial> _effectiveMaterialRepository =
        unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial>();

    public override async Task<Result<bool>> Handle(
        DeleteEffectiveMaterialCommand request,
        CancellationToken cancellationToken
    )
    {
        var effectiveMaterial = await _effectiveMaterialRepository.FindAsync(
            em => em.Id == request.Id,
            Include: em =>
                em.Include(em => em.CommonUses)
                    .Include(em => em.OffLabelUses)
                    .Include(em => em.SideEffects)
                    .Include(em => em.FoodInteractions)
                    .Include(em => em.DiseaseInteraction)
                    .Include(em => em.EM_CrossSelling)
                    .Include(em => em.EM_DrugInteractions)
        );

        if (effectiveMaterial == null)
            return Result<bool>.Fail(Messages.EffectiveMaterialNotFound);

        if (
            effectiveMaterial.CommonUses.Count != 0
            || effectiveMaterial.OffLabelUses.Count != 0
            || effectiveMaterial.SideEffects.Count != 0
            || effectiveMaterial.FoodInteractions.Count != 0
            || effectiveMaterial.DiseaseInteraction.Count != 0
            || effectiveMaterial.EM_CrossSelling.Count != 0
            || effectiveMaterial.EM_DrugInteractions.Count != 0
        )
            return Result<bool>.Fail(Messages.RelationExists);

        effectiveMaterial.Is_Deleted = true;

        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
