using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Commands.Delete;

public class DeleteUnitCommandHandler(IUnitOfWork unitOfWork)
 : BaseHandler<DeleteUnitCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Unit.Unit> _unitRepository = unitOfWork.GetRepository<Domain.Entities.Unit.Unit>();
    public override async Task<Result<bool>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = await _unitRepository.FindAsync(
            f => f.Id == request.Id);

        if (unit == null)
            return Result<bool>.Fail(Messages.NotFound);

        unit.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
