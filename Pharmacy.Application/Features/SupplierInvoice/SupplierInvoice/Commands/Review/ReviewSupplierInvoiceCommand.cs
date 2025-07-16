using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.Review;

public class ReviewSupplierInvoiceCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }
    public List<ReviewInvoiceItemDto> ReviewedItems { get; set; } = [];
}

public class ReviewInvoiceItemDto
{
    public Guid Id { get; set; }
    public decimal ReviewedQuantity { get; set; }
}
