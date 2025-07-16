using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.AddTransaction;

public class AddTransactionCommandValidator : AbstractValidator<AddTransactionCommand>
{
    public AddTransactionCommandValidator(IUnitOfWork unitOfWork)
    {

        RuleFor(x => x.SupplierInvoiceId)
            .NotEmpty().MustExistSupplierInvoice(unitOfWork);
            
        RuleFor(x => x.ShiftWalletId)
        .NotEmpty()
        .WithMessage(Messages.ShiftWalletDoesNotExist); 

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage(Messages.AmountMustBeGreaterThanZero);

        RuleFor(x => x.PaymentDate)
            .NotEmpty().WithMessage(Messages.PaymentDateIsRequired);

        RuleFor(x => x.PaymentMethod)
            .NotNull().WithMessage(Messages.PaymentMethodIsRequired);
    }
}
