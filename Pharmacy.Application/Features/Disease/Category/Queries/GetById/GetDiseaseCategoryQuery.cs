using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Category.Queries.GetById;

public class GetDiseaseCategoryQuery : IRequest<Result<GetDiseaseCategoryResponse>>
{
    public Guid Id { get; set; }
}
