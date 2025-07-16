using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.EmailService;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Create;

public class CreateAccountCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole>  roleManager, IMapper mapper, IEmailService emailService)
 : BaseHandler<CreateAccountCommand, Result<string>>
 {
     private readonly UserManager<ApplicationUser> _userManager = userManager;
     private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
     private readonly IEmailService _emailService = emailService;

     public override async Task<Result<string>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
     {
         var user = mapper.Map<ApplicationUser>(request);

         var result = await _userManager.CreateAsync(user, request.Password);
         if (!result.Succeeded)
            return Result<string>.Fail(result.Errors.Select(x => x.Description).ToList());

         var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
         if (role == null)
            return Result<string>.Fail(Messages.RoleNotFound);

        result = await _userManager.AddToRoleAsync(user, role.Name!);
        if (!result.Succeeded)
            return Result<string>.Fail(Messages.SomethingWentWrong);

        await _emailService.SendEmailAsync(user.Full_Name, user.Email!, user.UserName!, request.Password);

        return Result<string>.Success(Messages.AccountCreatedSuccessfully);
     }
 }


