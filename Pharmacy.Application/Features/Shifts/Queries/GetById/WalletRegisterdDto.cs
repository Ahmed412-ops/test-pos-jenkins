namespace Pharmacy.Application.Features.Shifts.Queries.GetById;

public class WalletRegisterdDto
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TransactionType { get; set; } = string.Empty;
    public required decimal OpeningBalance { get; set; }
    public decimal TotalSales { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal ExpectedClosingAmount { get; set; }
    public decimal? ActualClosingBalance { get; set; }
    public decimal? Difference { get; set; }
    public string? DifferenceReason { get; set; }
}
