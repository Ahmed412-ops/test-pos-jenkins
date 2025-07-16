using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.Commands.Create;

public class CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
    : BaseHandler<CreateRoleCommand, Result<string>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;


    public override async Task<Result<string>> Handle(CreateRoleCommand request,CancellationToken cancellationToken)
    {
        var role = mapper.Map<ApplicationRole>(request);

        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
            return Result<string>.Fail(Messages.SomethingWentWrong);

        return Result<string>.Success(Messages.RoleCreatedSuccessfully);
    }

}
