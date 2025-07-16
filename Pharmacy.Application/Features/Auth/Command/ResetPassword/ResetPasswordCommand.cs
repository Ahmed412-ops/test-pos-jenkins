using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Auth.Command.ResetPassword;

public class ResetPasswordCommand : IRequest<Result<string>>
{
  public required string UserName { get; set; }
  public required string OldPassword { get; set; }   
  public required string NewPassword { get; set; }
}
