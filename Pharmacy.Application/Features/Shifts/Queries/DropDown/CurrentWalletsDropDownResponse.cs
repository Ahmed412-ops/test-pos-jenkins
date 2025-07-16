namespace Pharmacy.Application.Features.Shifts.Queries.DropDown;

public class CurrentWalletsDropDownResponse
{
    public Guid ShiftId { get; set; }
    public Guid ShiftWalletId { get; set; }
    public Guid WalletId { get; set; }
    public required string Name { get; set; }
}
