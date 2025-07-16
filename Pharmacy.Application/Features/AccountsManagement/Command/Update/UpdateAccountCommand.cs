using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Update;

public class UpdateAccountCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? StartDate { get; set; } // Start date of the user to work in the system
    public DateTime? EndDate { get; set; } // End date of the user to work in the system
}
