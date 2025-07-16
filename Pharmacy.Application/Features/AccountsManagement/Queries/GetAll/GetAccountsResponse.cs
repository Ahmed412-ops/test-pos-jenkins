namespace Pharmacy.Application.Features.AccountsManagement.Queries;

public class GetAccountsResponse
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Role { get; set; }
    public Guid RoleId { get; set; }
    public required string UserName { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? StartDate { get; set; } // Start date of the user to work in the system
    public DateTime? EndDate { get; set; } // End date of the user to work in the system
    public bool? IsActive { get; set; }

}
