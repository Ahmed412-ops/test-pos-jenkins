using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Queries.GetById;

public class GetUseQuery : IRequest<Result<GetUseResponse>>
{
    public Guid Id { get; set; }  
}

