using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Commands.Update;

public class UpdateDosageFormCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateDosageFormCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.DosageForm.DosageForm> _dosageFormRepository = unitOfWork.GetRepository<Domain.Entities.DosageForm.DosageForm>();

    public override async Task<Result<string>> Handle(UpdateDosageFormCommand request, CancellationToken cancellationToken)
    {
        var dosageForm = await _dosageFormRepository.FindAsync(
            s => s.Id == request.Id);

        if (dosageForm == null)
            return Result<string>.Fail(Messages.DosageFormNotFound);

        mapper.Map(request, dosageForm);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}

