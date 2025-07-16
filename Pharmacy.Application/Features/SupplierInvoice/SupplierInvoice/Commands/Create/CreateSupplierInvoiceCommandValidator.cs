namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Create;

using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Enum;

public class CreateSupplierInvoiceCommandValidator : AbstractValidator<CreateSupplierInvoiceCommand>
{
    public CreateSupplierInvoiceCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.InvoiceNumber)
            .NotEmpty().WithMessage(Messages.InvoiceNumberIsRequired)
            .MustBeUniqueSupplierInvoiceNumber(unitOfWork).WithMessage(Messages.InvoiceNumberAlreadyExists);
        
        RuleFor(x => x.SupplierId)
            .NotEmpty()
            .MustExistSupplier(unitOfWork);
        
        RuleFor(x => x.InvoiceDate)
            .NotEmpty().WithMessage(Messages.InvoiceDateIsRequired);
        
        
        // When invoice is paid or partially paid, require PaymentDate and PaymentMethod.
        When(x => x.AmountPaid > 0, () =>
        {
            RuleFor(x => x.PaymentDate)
                .NotEmpty().WithMessage(Messages.PaymentDateIsRequired);
            
            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage(Messages.PaymentMethodIsRequired);
            
            RuleFor(x => x.ShiftWalletId)
                .NotEmpty().WithMessage(Messages.ShiftWalletDoesNotExist);
        });
        //// Validate that at least one invoice item is provided.
        When(x => !x.IsRecevied, () =>
        {
            RuleFor(x => x.InvoiceItems)
                .NotEmpty().WithMessage(Messages.InvoiceItemsRequired);
        });

        // Use a child validator for each invoice item.
        RuleForEach(x => x.InvoiceItems)
            .SetValidator(new CreateSupplierInvoiceItemDtoValidator(unitOfWork));
    }
}
