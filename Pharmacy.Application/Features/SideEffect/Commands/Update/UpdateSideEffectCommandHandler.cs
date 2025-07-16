using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Commands.Update;

public class UpdateSideEffectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateSideEffectCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.SideEffects.SideEffect> _sideEffectRepository = unitOfWork.GetRepository<Domain.Entities.SideEffects.SideEffect>();

    public override async Task<Result<string>> Handle(UpdateSideEffectCommand request, CancellationToken cancellationToken)
    {
        var sideEffect = await _sideEffectRepository.FindAsync(
            s => s.Id == request.Id);

        if (sideEffect == null)
            return Result<string>.Fail(Messages.NotFound);

        mapper.Map(request, sideEffect);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
