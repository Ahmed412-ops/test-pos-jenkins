namespace Pharmacy.Application.Services.Abstraction.AutoPaymentService;

public interface IAutoPaymentService
{
    Task<PaymentDistributionResult> DistributePaymentAsync(Guid customerId,Guid ShiftWalletId, decimal paymentAmount);
}

public class PaymentDistributionResult(
    List<AppliedPayment> appliedPayments,
    decimal remainingCredit = 0
)
{
    public List<AppliedPayment> AppliedPayments { get; set; } = appliedPayments;
    public decimal RemainingCredit { get; set; } = remainingCredit;
}

public class AppliedPayment(Guid prescriptionId, decimal amountPaid, decimal remainingBalance)
{
    public Guid PrescriptionId { get; set; } = prescriptionId;
    public decimal AmountPaid { get; set; } = amountPaid;
    public decimal RemainingBalance { get; set; } = remainingBalance;
}
