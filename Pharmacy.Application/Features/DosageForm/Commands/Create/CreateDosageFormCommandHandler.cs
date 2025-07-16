using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Commands.Create;

public class CreateDosageFormCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateDosageFormCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.DosageForm.DosageForm> _dosageFormRepository = unitOfWork.GetRepository<Domain.Entities.DosageForm.DosageForm>();

    public override async Task<Result<string>> Handle(CreateDosageFormCommand request, CancellationToken cancellationToken)
    {
        var dosageForm = mapper.Map<Domain.Entities.DosageForm.DosageForm>(request);
        await _dosageFormRepository.AddAsync(dosageForm);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
