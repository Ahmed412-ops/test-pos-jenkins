using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Delete;

public class DeleteEffectiveMaterialCategoryCommandHandler(IUnitOfWork unitOfWork)
 : BaseHandler<DeleteEffectiveMaterialCategoryCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory> _effectiveMaterialCategoryRepository = unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory>();
    public override async Task<Result<bool>> Handle(DeleteEffectiveMaterialCategoryCommand request, CancellationToken cancellationToken)
    {
        var effectiveMaterialCategory = await _effectiveMaterialCategoryRepository.FindAsync(
            f => f.Id == request.Id,
            Include: f => f.Include(f => f.EffectiveMaterials));

        if (effectiveMaterialCategory == null)
            return Result<bool>.Fail(Messages.NotFound);
        
        if (effectiveMaterialCategory.EffectiveMaterials.Count != 0)
            return Result<bool>.Fail(Messages.RelationExists);

        effectiveMaterialCategory.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}

