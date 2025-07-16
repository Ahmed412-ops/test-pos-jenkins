using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Queries.GetById;

public class GetSideEffectQuery : IRequest<Result<GetSideEffectResponse>>
{
    public Guid Id { get; set; }    
}
