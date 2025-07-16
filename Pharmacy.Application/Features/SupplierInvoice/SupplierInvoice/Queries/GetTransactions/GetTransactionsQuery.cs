using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetTransactions;

public class GetTransactionsQuery : Pagination, IRequest<Result<PaginationResponse<GetTransactionsResponse>>>
{
    public Guid InvoiceId { get; set; }    
}
