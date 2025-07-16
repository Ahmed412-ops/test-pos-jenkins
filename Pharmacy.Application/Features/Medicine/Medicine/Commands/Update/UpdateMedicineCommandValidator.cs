using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;

namespace Pharmacy.Application.Features.Medicine.Medicine.Commands.Update;

public class UpdateMedicineCommandValidator : UpdateBaseCommandValidator<UpdateMedicineCommand, Domain.Entities.Medicine.Medicine>
{
    public UpdateMedicineCommandValidator(IUnitOfWork context)
        : base(context) 
    {
        Include(new CreateMedicineCommandValidator(context, false));        
    }
}
