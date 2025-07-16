using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Queries.GetAll;

public class GetShiftsQuery : Pagination, IRequest<Result<PaginationResponse<GetShiftsResponse>>>
{
    public string? OpenedBy { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool? IsOpen { get; set; }
}
