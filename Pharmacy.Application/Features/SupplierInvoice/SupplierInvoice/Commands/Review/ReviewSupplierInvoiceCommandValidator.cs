using FluentValidation;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Review;

public class ReviewSupplierInvoiceCommandValidator : AbstractValidator<ReviewSupplierInvoiceCommand>
{
    public ReviewSupplierInvoiceCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage(Messages.InvoiceIdIsRequired);
        
        RuleFor(command => command.ReviewedItems)
            .NotEmpty().WithMessage(Messages.ReviewedItemsRequired);
        
        RuleForEach(command => command.ReviewedItems)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Id)
                    .NotEmpty().WithMessage(Messages.InvoiceItemIdIsRequired);
            });

    }
}