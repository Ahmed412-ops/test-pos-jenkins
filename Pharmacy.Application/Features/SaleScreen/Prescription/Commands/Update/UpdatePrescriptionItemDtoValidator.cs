using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Create;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Update;

public class UpdatePrescriptionItemDtoValidator 
: AbstractValidator<UpdatePrescriptionItemDto>
{
    public UpdatePrescriptionItemDtoValidator(IUnitOfWork unitOfWork)
    {
        Include(new CreatePrescriptionItemDtoValidator(unitOfWork));
        RuleFor(i => i.PrescriptionItemId)
            .NotEmpty()
            .WithMessage(Messages.PrescriptionItemIdRequired);

    }
}