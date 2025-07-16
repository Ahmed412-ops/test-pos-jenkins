using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Supplier.Commands.Create;

public class CreateSupplierCommandValidator : BaseCommandValidator<CreateSupplierCommand, Domain.Entities.Supplier.Supplier>
{
    public CreateSupplierCommandValidator(IUnitOfWork unitOfWork, bool checkName = true)
        : base(unitOfWork, checkName)
    {
        RuleFor(x => x.SupplierType)
            .IsInEnum()
            .WithMessage(Messages.InvalidSupplierType);

        RuleForEach(x => x.Contacts)
            .ChildRules(contact =>
            {
                contact.RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage(Messages.ContactNameIsRequiredForEachContact);
                    
                contact.RuleFor(c => c.PhoneNumber)
                    .NotEmpty()
                    .WithMessage(Messages.PhoneNumberIsRequiredForEachContact);

            });
    }
}
