using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Update;

public class UpdateEffectiveMaterialCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<UpdateEffectiveMaterialCategoryCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory> _effectiveMaterialCategoryRepository = unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory>();


    public override async Task<Result<string>> Handle(UpdateEffectiveMaterialCategoryCommand request, CancellationToken cancellationToken)
    {
        var effectiveMaterialCategory = await _effectiveMaterialCategoryRepository.FindAsync(
            d => d.Id == request.Id);

        if (effectiveMaterialCategory == null)
            return Result<string>.Fail(Messages.NotFound);

        mapper.Map(request, effectiveMaterialCategory);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
