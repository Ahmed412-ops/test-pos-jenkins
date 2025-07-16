using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Dto.StockHistory;

public class StockHistoryLogDto
{
    public Guid MedicineId { get; set; }
    public DateTime TransactionDate { get; set; }
    public StockTransactionType TransactionType { get; set; }
    public decimal QuantityChange { get; set; }
    public decimal UpdatedStockLevel { get; set; }
    public Guid PerformedById { get; set; }
    public string? TransactionReference { get; set; }
    public string? ReasonForChange { get; set; }
}
