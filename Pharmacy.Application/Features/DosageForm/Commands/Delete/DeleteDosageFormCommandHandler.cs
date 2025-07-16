using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Commands.Delete;

public class DeleteDosageFormCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteDosageFormCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.DosageForm.DosageForm> _dosageFormRepository = unitOfWork.GetRepository<Domain.Entities.DosageForm.DosageForm>();
    public override async Task<Result<bool>> Handle(DeleteDosageFormCommand request, CancellationToken cancellationToken)
    {
        var dosageForm = await _dosageFormRepository.FindAsync(d => d.Id == request.Id);
        if (dosageForm == null)
            return Result<bool>.Fail(Messages.NotFound);

        dosageForm.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
