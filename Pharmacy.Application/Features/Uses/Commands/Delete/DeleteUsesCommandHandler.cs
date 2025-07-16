using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Commands.Delete;

public class DeleteUsesCommandHandler(IUnitOfWork unitOfWork)
 : BaseHandler<DeleteUsesCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Uses.Use> _useRepository = unitOfWork.GetRepository<Domain.Entities.Uses.Use>();
    public override async Task<Result<bool>> Handle(DeleteUsesCommand request, CancellationToken cancellationToken)
    {
        var use = await _useRepository.FindAsync(
            f => f.Id == request.Id,
            Include: f => f.Include(f => f.EffectiveMaterialCommonUses)
                .Include(f => f.EffectiveMaterialOffLabelUses));

        if (use == null)
            return Result<bool>.Fail(Messages.NotFound);

        if (use.EffectiveMaterialCommonUses.Count != 0 || use.EffectiveMaterialOffLabelUses.Count != 0)
            return Result<bool>.Fail(Messages.RelationExists);

        use.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
