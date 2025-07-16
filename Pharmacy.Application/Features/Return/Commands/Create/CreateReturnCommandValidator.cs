using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Return.Commands.Create;

public class CreateReturnCommandValidator : AbstractValidator<CreateReturnCommand>
{
    public CreateReturnCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PrescriptionId)
            .NotEmpty()
            .WithMessage(Messages.IdIsRequired)
            .MustExistPrescription(unitOfWork);

        RuleForEach(x => x.Items)
            .ChildRules(items =>
            {
                items
                    .RuleFor(i => i.PrescriptionItemId)
                    .NotEmpty()
                    .WithMessage(Messages.IdIsRequired)
                    .MustExistPrescriptionItem(unitOfWork);

                items
                    .RuleFor(i => i.QuantityReturned)
                    .GreaterThan(0)
                    .WithMessage(Messages.QuantityMustBeGreaterThanZero);
            });
    }
}
