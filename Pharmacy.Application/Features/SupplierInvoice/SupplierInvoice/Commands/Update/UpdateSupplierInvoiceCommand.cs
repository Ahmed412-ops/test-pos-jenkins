using MediatR;
using Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Create;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Update;

public class UpdateSupplierInvoiceCommand : CreateSupplierInvoiceCommand, IRequest<Result<string>>
{
    public Guid Id { get; set; }
}
