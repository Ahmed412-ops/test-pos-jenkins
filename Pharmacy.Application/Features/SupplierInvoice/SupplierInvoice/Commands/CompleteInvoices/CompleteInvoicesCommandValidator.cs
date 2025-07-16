using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.CompleteInvoices;

public class CompleteInvoicesCommandValidator : AbstractValidator<CompleteInvoicesCommand>
{
    public CompleteInvoicesCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleForEach(x => x.InvoiceIds)
            .MustExistSupplierInvoice(unitOfWork);
    }
}

