using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Category.Queries.GetAll;

public class GetDiseaseCategoriesQuery : Pagination, IRequest<Result<PaginationResponse<GetDiseaseCategoriesResponse>>>
{
    public string? Name { get; set; }      
}
