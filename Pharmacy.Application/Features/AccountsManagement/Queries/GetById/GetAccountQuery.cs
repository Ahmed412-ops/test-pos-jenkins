using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.AccountsManagement.Queries.GetById;

public class GetAccountQuery : IRequest<Result<GetAccountResponse>>
{
    public Guid Id { get; set; }
}
