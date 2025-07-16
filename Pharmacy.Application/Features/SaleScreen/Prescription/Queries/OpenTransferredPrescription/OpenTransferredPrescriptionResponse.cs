using Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;
using Pharmacy.Application.Features.SaleScreen.Prescription.Queries.OpenTransferredPrescription.Responses;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.OpenTransferredPrescription;

public class OpenTransferredPrescriptionResponse
{
    public Guid Id { get; set; }
    public int InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountDue { get; set; }
    public decimal CashbackEarned { get; set; }
    public decimal CashbackUsed { get; set; }
    public decimal CreditUsed { get; set; }
    public int TotalItems { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public GetCustomerByIdResponse? Customer { get; set; } 
    public ShiftWalletResponse ShiftWallet { get; set; } = new();
    public List<GetPrescriptionItemResponse> Items { get; set; } = [];
}