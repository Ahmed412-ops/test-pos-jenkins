using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.AccountsManagement.Command.ChangeActivation;

public class ChangeActivationCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }
}