using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Queries.GetById;

public class GetUnitQuery : IRequest<Result<GetUnitResponse>>
{
    public Guid Id { get; set; }  
}
