using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Stock.Return.Commands.Create;

public class CreateMedicationReturnCommandValidator : AbstractValidator<CreateMedicationReturnCommand>
{
    public CreateMedicationReturnCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.ReturnReferenceNumber)
            .NotEmpty().WithMessage(Messages.ReturnReferenceNumberIsRequired);

        RuleFor(x => x.SupplierId)
            .NotEmpty()
            .MustExistSupplier(unitOfWork);

        RuleFor(x => x.ReturnStatus)
            .IsInEnum().WithMessage(Messages.ReturnStatusNotAvailable);;

        RuleFor(x => x.ReturnItems)
            .NotEmpty().WithMessage(Messages.ReturnItemsAreRequired);

        RuleForEach(x => x.ReturnItems)
            .SetValidator(new MedicationReturnItemDtoValidator(unitOfWork));

    }
}
