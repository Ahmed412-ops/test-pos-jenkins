using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.AccountsManagement.Queries;

public class GetAccountsQuery : Pagination, IRequest<Result<PaginationResponse<GetAccountsResponse>>>
{
    public string? UserNameOrPhone { get; set; }
    public string? Role { get; set; }
    public bool? IsActive { get; set; }    
    public DateTime? StartDate { get; set; } // Start date of the user to work in the system
    public DateTime? EndDate { get; set; } // End date of the user to work in the system
}
