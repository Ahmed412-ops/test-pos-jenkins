using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Commands.Delete;

public class DeleteSideEffectCommandHandler(IUnitOfWork unitOfWork)
 : BaseHandler<DeleteSideEffectCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.SideEffects.SideEffect> _sideEffectRepository = unitOfWork.GetRepository<Domain.Entities.SideEffects.SideEffect>();
    public override async Task<Result<bool>> Handle(DeleteSideEffectCommand request, CancellationToken cancellationToken)
    {
        var sideEffect = await _sideEffectRepository.FindAsync(
            f => f.Id == request.Id,
            Include: f => f.Include(f => f.EffectiveMaterialSideEffects));

        if (sideEffect == null)
            return Result<bool>.Fail(Messages.NotFound);
        
        if (sideEffect.EffectiveMaterialSideEffects.Count != 0)
            return Result<bool>.Fail(Messages.RelationExists);

        sideEffect.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
