using MediatR;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.AddTransaction;

public class AddTransactionCommand : IRequest<Result<string>>
{
    public Guid SupplierInvoiceId { get; set; }
    public Guid ShiftWalletId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public string? Notes { get; set; }
}
