using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetById;

public class GetSupplierInvoiceQuery : IRequest<Result<GetSupplierInvoiceResponse>>
{
    public Guid Id { get; set; }    
}
