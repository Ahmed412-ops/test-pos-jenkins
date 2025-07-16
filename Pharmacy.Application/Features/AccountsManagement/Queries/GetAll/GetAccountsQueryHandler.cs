using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Queries;

public class GetAccountsQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    : BaseHandler<GetAccountsQuery, Result<PaginationResponse<GetAccountsResponse>>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public override async Task<Result<PaginationResponse<GetAccountsResponse>>> Handle(
        GetAccountsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = _userManager
            .Users.Include(a => a.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(a =>
                !a.Is_Deleted && a.UserRoles.All(ur => ur.Role.Name != nameof(UserRole.SuperAdmin))
            )
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.UserNameOrPhone))
            query = query.Where(a =>
                (a.Full_Name != null && a.Full_Name.Contains(request.UserNameOrPhone))
                || (a.PhoneNumber != null && a.PhoneNumber.Contains(request.UserNameOrPhone))
            );

        if (!string.IsNullOrEmpty(request.Role))
            query = query.Where(a => a.UserRoles.Any(ur => ur.Role.Name!.Contains(request.Role)));

        if (request.IsActive.HasValue)
            query = query.Where(a => a.Is_Active == request.IsActive);

        if (request.StartDate.HasValue)
            query = query.Where(a => a.Created_At >= request.StartDate);

        if (request.EndDate.HasValue)
            query = query.Where(a => a.Created_At <= request.EndDate);

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetAccountsResponse>(a))
            .Paginate(request)
            .ToList();
        return Result<PaginationResponse<GetAccountsResponse>>.Success(
            new PaginationResponse<GetAccountsResponse> { Data = response, Count = count }
        );
    }
}
