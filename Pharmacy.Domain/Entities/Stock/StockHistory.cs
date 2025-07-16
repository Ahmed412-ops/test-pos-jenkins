using Pharmacy.Domain.Entities.Identity;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.Entities.Stock;

public class StockHistory : BaseEntity
{
    public Guid MedicineId { get; set; }
    public Medicine.Medicine Medicine { get; set; } = null!;
    public DateTime TransactionDate { get; set; }
    public StockTransactionType TransactionType { get; set; }
    public Guid PerformedById { get; set; }
    public ApplicationUser PerformedBy { get; set; } = null!;
    public decimal QuantityChange { get; set; }     // (positive or negative)
    public decimal UpdatedStockLevel { get; set; }     // Updated stock level after this transaction
    public string? ReasonForChange { get; set; }
    // Optional reference such as an invoice or return number
    public string? TransactionReference { get; set; }
}
