namespace Pharmacy.Application.Features.Shifts.CashExpenses.Queries.GetAll;

public class GetCashExpensesResponse
{
    public Guid Id { get; set; }
    public Guid ShiftId { get; set; }
    public string? Full_Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDateTime { get; set; }
    public string? CategoryName { get; set; }
    public bool IsOpen { get; set; }

}

