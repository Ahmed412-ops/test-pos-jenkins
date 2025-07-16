using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Create;

public class CreateEffectiveMaterialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<CreateEffectiveMaterialCommand, Result<string>> 
{
    private readonly IGenericRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial> _effectiveMaterialRepository = unitOfWork.GetRepository<Domain.Entities.EffectiveMaterial.EffectiveMaterial>();
    public override async Task<Result<string>> Handle(CreateEffectiveMaterialCommand request, CancellationToken cancellationToken)
    {
        var effectiveMaterial = mapper.Map<Domain.Entities.EffectiveMaterial.EffectiveMaterial>(request);

        await _effectiveMaterialRepository.AddAsync(effectiveMaterial);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
