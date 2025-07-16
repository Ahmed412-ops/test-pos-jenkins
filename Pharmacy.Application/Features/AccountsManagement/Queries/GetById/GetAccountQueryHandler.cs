using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Queries.GetById;

public class GetAccountQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper) : BaseHandler<GetAccountQuery, Result<GetAccountResponse>>
{

    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public override async Task<Result<GetAccountResponse>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Include(a => a.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (user == null)
            return Result<GetAccountResponse>.Fail(Messages.UserNotFound);

        var response = mapper.Map<GetAccountResponse>(user);
        return Result<GetAccountResponse>.Success(response);
    }
}

