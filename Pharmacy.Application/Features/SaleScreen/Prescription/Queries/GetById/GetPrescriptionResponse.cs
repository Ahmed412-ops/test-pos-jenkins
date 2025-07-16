namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetById;

public class GetPrescriptionResponse
{
    public Guid Id { get; set; }
    public string? CustomerName { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal AmountDue { get; set; }
    public decimal CashbackEarned { get; set; }
    public decimal CashbackUsed { get; set; }
    public decimal CreditUsed { get; set; }
    public DateTime Created_At { get; set; }
    public int TotalItems { get; set; }
    public string? CreatedBy { get; set; }
    public string? TransferredBy { get; set; }
    public List<GetPrescriptionItemResponse> PrescriptionItems { get; set; } = [];
    public List<PrescriptionTransactionDto> Transactions { get; set; } = [];
}

public class GetPrescriptionItemResponse
{
    public Guid Id { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public string MedicineUnitName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal AppliedDiscount { get; set; }
    public decimal TotalPrice { get; set; }
    public int ReturnedQuantity { get; set; } = 0;
    public int AvailableForReturn { get; set; } = 0;
}

public class PrescriptionTransactionDto
{
    public Guid Id { get; set; }
    public int InvoiceNumber { get; set; }
    public decimal AmountPaid { get; set; }
    public Guid ShiftWalletId { get; set; }
    public DateTime Created_At { get; set; }
}
