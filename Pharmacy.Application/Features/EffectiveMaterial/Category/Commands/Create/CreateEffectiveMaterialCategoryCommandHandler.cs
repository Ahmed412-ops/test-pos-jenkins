using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Create;

public class CreateEffectiveMaterialCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateEffectiveMaterialCategoryCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory> _effectiveMaterialCategoryRepository = unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory>();


    public override async Task<Result<string>> Handle(CreateEffectiveMaterialCategoryCommand request, CancellationToken cancellationToken)
    {
        var effectiveMaterialCategory = mapper.Map<Domain.Entities.EffectiveMaterial.EffectiveMaterialCategory>(request);

        await _effectiveMaterialCategoryRepository.AddAsync(effectiveMaterialCategory);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}

