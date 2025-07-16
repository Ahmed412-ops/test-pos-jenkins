using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Create;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Update;

public class UpdateSupplierInvoiceCommandValidator : AbstractValidator<UpdateSupplierInvoiceCommand>
{
    public UpdateSupplierInvoiceCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
                   .NotEmpty().WithMessage(Messages.IdIsRequired);

        Include(new CreateSupplierInvoiceCommandValidator(unitOfWork));
        RuleFor(x => x.InvoiceNumber)
            .NotEmpty().WithMessage(Messages.InvoiceNumberIsRequired)
            .MustAsync(async (command, invoiceNumber, cancellation) =>
            {
                var isExist = await unitOfWork.GetRepository<Domain.Entities.SupplierInvoice.SupplierInvoice>()
                    .IsExistsAsync(i => i.InvoiceNumber == invoiceNumber && i.Id != command.Id);

                return !isExist; 
            })
            .WithMessage(Messages.InvoiceNumberAlreadyExists);
    }
}
