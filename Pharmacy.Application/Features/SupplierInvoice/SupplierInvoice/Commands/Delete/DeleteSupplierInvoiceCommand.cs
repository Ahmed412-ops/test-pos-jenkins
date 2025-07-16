using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Delete;

public class DeleteSupplierInvoiceCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
