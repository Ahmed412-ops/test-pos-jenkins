 using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AutoPayment;

public class AutoPaymentCommand : IRequest<Result<AutoPaymentResponse>>
{
    public Guid CustomerId { get; set; }
    public Guid ShiftWalletId { get; set; }
    public decimal AmountPaid { get; set; }
}
