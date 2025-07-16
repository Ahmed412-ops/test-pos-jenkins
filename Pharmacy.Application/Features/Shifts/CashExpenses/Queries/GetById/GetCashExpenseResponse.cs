namespace Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetById;

public class GetCashExpenseResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDateTime { get; set; }
    public string? PaidTo { get; set; }
    public string? Notes { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string ShiftWallet { get; set; } = string.Empty;
}
