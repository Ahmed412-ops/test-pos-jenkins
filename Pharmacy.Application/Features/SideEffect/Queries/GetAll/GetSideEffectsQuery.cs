using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SideEffect.Queries.GetAll;

public class GetSideEffectsQuery : Pagination, IRequest<Result<PaginationResponse<GetSideEffectsResponse>>>
{
    public string? Name { get; set; }
}

