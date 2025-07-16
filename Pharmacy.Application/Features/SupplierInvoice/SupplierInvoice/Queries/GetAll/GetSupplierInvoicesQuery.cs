using MediatR;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.GetAll;

public class GetSupplierInvoicesQuery : Pagination, IRequest<Result<InvoicesPaginationResponse>>
{
    public string? InvoiceNumber { get; set; }
    public string? SupplierName { get; set; }
    public DateTime? InvoiceStartDate { get; set; }
    public DateTime? InvoiceEndDate { get; set; }
    public DateTime? DueStartDate { get; set; }
    public DateTime? DueEndDate { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
    public bool? IsReviewed { get; set; }
    public bool? IsReceived { get; set; }

}
