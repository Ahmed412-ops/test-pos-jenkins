namespace Pharmacy.Application.Dto.Common.Queries;

public class CashTransactionsHistoryDto
{
    public Guid Id { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string WalletRegister { get; set; } = string.Empty;
    public string? ExpenseCategory { get; set; }
    public string PerformedBy { get; set; } = string.Empty;
    public string? CustomerName { get; set; }
    public string? Notes { get; set; }
    public DateTime Date { get; set; }
}
