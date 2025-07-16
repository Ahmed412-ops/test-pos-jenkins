using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Delete;

public class DeleteAccountCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }
}
