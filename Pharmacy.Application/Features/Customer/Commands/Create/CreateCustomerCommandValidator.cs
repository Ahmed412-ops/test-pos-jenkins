using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommandValidator : BaseCommandValidator<CreateCustomerCommand, Domain.Entities.Customers.Customer>
{

    public CreateCustomerCommandValidator(IUnitOfWork unitOfWork)
         : base(unitOfWork)
    {
        RuleForEach(c => c.ChronicMedicineIds)
            .MustExistMedicine(unitOfWork);

        RuleForEach(c => c.ChronicDiseaseIds)
            .MustExistDisease(unitOfWork);
    }
}

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(a => a.City).NotEmpty().WithMessage(Messages.CityRequired);
        RuleFor(a => a.StreetName).NotEmpty().WithMessage(Messages.StreetNameRequired);
    }
}

public class PhoneNumberValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberValidator()
    {
        RuleFor(p => p.Number).NotEmpty().WithMessage(Messages.PhoneNumberIsRequiredForEachContact);
    }
}
