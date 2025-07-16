namespace Pharmacy.Application.Features.Shifts.Queries.GetAll;

public class GetShiftsResponse
{
    public Guid Id { get; set; }
    public string OpenedBy { get; set; } = null!;
    public DateTime OpenedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public decimal OpeningBalance { get; set; }
    public decimal IncomingTotal { get; set; }
    public decimal OutgoingTotal { get; set; }
    public decimal NetCashMovement { get; set; }
    public decimal ExpectedClosingBalanceTotal { get; set; }
    public decimal? ActualClosingBalanceTotal { get; set; }
    public bool IsOpen { get; set; }
}
