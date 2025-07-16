using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Queries.GetAll;

public class GetFoodQuery : Pagination, IRequest<Result<PaginationResponse<GetFoodResponse>>>
{
    public string? Name { get; set; }
}
