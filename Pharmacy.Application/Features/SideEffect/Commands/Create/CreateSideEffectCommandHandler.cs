using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Commands.Create;

public class CreateSideEffectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateSideEffectCommand, Result<string>>
{
   private readonly IGenericRepository<Domain.Entities.SideEffects.SideEffect> _sideEffectRepository = unitOfWork.GetRepository<Domain.Entities.SideEffects.SideEffect>();

   public override async Task<Result<string>> Handle(CreateSideEffectCommand request, CancellationToken cancellationToken)
   {
      var sideEffect = mapper.Map<Domain.Entities.SideEffects.SideEffect>(request);
      await _sideEffectRepository.AddAsync(sideEffect);
      await unitOfWork.SaveChangesAsync();
      return Result<string>.Success(Messages.SuccessfullyCreated);
   }
}
