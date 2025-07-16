namespace Pharmacy.Application.Features.Shifts.Queries.GetById;

public class GetShiftResponse
{
    public Guid Id { get; set; }
    public string OpenedBy { get; set; } = null!;
    public DateTime OpenedAt { get; set; }
    public string? ClosedBy { get; set; }
    public DateTime? ClosedAt { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal IncomingTotal { get; set; }
    public decimal OutgoingTotal { get; set; }
    public decimal NetCashMovement { get; set; }
    public decimal ExpectedClosingBalanceTotal { get; set; }
    public decimal? ActualClosingBalanceTotal { get; set; }
    public decimal? Difference { get; set; }
    public string? Notes { get; set; }
    public bool IsOpen { get; set; }
    public List<ShiftTransactionDto> IncomingTransactions { get; set; } = [];
    public List<ShiftTransactionDto> OutgoingTransactions { get; set; } = [];
    public List<WalletRegisterdDto> WalletRegisters { get; set; } = [];
}
