using Pharmacy.Application.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetAll;

public class InvoicesPaginationResponse : PaginationResponse<GetSupplierInvoicesResponse>
{
    public decimal TotalRemaining { get; set; }
}
