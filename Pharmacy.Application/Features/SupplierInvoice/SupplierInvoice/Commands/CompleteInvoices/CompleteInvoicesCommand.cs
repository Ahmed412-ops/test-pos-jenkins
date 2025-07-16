using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.CompleteInvoices;

public class CompleteInvoicesCommand : IRequest<Result<string>>
{
   public List<Guid> InvoiceIds { get; set; } = [];
}
