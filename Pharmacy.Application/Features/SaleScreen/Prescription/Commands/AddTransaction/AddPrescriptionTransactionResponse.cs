namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.AddTransaction;

public class AddPrescriptionTransactionResponse
{
    public Guid PrescriptionId { get; set; }
    public int InvoiceNumber { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountDue { get; set; }
    public DateTime Created_At { get; set; } = DateTime.UtcNow;
}
