namespace Pharmacy.Application.Features.Stock.History.Queries.GetAll;

public class GetStockHistoryResponse
{
    public Guid Id { get; set; }
    public string MedicineName { get; set; } = null!;
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; } = null!;
    public string PerformedBy { get; set; } = null!;
    public decimal QuantityChange { get; set; }
    public decimal UpdatedStockLevel { get; set; }
    public string? ReasonForChange { get; set; }
    public string? TransactionReference { get; set; }
}
