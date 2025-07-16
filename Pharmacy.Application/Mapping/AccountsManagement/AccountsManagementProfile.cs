using Pharmacy.Application.Features.AccountsManagement.Command.Create;
using Pharmacy.Application.Features.AccountsManagement.Command.Update;
using Pharmacy.Application.Features.AccountsManagement.Queries;
using Pharmacy.Application.Features.AccountsManagement.Queries.GetById;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Mapping.AccountsManagement;

public class AccountsManagementProfile : MappingProfileBase
{
  public AccountsManagementProfile()
  {
    CreateMap<CreateAccountCommand, ApplicationUser>()
      .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

    CreateMap<ApplicationUser, GetAccountsResponse>()
      .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role.Name))
      .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().RoleId));

    CreateMap<ApplicationUser, GetAccountResponse>()
      .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role.Name))
      .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().RoleId));

    CreateMap<UpdateAccountCommand, ApplicationUser>()
      .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

  }
}
