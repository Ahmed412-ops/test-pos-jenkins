using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Customer.Commands.Create;
using Pharmacy.Application.Helper.Extensions;

namespace Pharmacy.Application.Features.Customer.Commands.Update;

public class UpdateCustomerCommandValidator : UpdateBaseCommandValidator<UpdateCustomerCommand, Domain.Entities.Customers.Customer>
{
    public UpdateCustomerCommandValidator(IUnitOfWork context) : base(context)
    {
            RuleForEach(c => c.ChronicMedicineIds)
        .MustExistMedicine(context);

            RuleForEach(c => c.ChronicDiseaseIds)
                .MustExistDisease(context);

        RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        RuleForEach(x => x.PhoneNumbers).SetValidator(new PhoneNumberValidator());
    }
}
