using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Queries.GetAll;

public class GetSymptomsQuery : Pagination, IRequest<Result<PaginationResponse<GetSymptomsResponse>>>
{
    public string? Name { get; set; }
}
