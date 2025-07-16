using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Disease.Disease.Commands.Create;

namespace Pharmacy.Application.Features.Disease.Disease.Commands.Update;

public class UpdateDiseaseCommandValidator : UpdateBaseCommandValidator<UpdateDiseaseCommand, Domain.Entities.Disease.Disease>
{
    public UpdateDiseaseCommandValidator(IUnitOfWork context)
        : base(context)
    {
        Include(new CreateDiseaseCommandValidator(context, false));
    }
}
