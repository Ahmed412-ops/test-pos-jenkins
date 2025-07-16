using MediatR;
using Pharmacy.Application.Features.Roles.Common;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Roles.Queries.GetById;

public class GetRoleQuery : IRequest<Result<GetRoleResponse>>
{
    public Guid Id { get; set; }    
}
