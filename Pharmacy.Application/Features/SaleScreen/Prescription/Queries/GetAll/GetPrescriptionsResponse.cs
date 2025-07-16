namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetAll;

public class GetPrescriptionsResponse
{
    public Guid Id { get; set; }
    public required int InvoiceNumber { get; set; }
    public string? CustomerName { get; set; }
    public DateTime PrescriptionDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountDue { get; set; }
    public decimal CashbackEarned { get; set; }
    public decimal CashbackUsed { get; set; }
}
