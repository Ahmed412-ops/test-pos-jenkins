namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AutoPayment;

public class AutoPaymentResponse
{
    public decimal TotalAmountApplied { get; set; }
    public decimal CreditBalance { get; set; }
    public List<PaymentDetailDto> AppliedPayments { get; set; } = [];
}

public class PaymentDetailDto
{
    public Guid PrescriptionId { get; set; }
    public decimal AppliedAmount { get; set; }
    public decimal RemainingBalance { get; set; }
}
