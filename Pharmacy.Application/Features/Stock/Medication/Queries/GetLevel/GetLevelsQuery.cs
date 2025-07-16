using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetLevel;

public class GetLevelsQuery : Pagination, IRequest<Result<PaginationResponse<GetLevelsResponse>>>
{
    public string? MedicineName { get; set; }
    public bool? IsBelowReorderPoint { get; set; }
}
