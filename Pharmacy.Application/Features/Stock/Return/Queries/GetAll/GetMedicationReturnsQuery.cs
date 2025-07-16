using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Return.Queries.GetAll;

public class GetMedicationReturnsQuery : Pagination, IRequest<Result<PaginationResponse<GetMedicationReturnsResponse>>>
{
    public string? ReturnReferenceNumber { get; set; }
    public string? SupplierName { get; set; }
    public DateTime? ReturnStartDate { get; set; }
    public DateTime? ReturnEndDate { get; set; }
    public ReturnStatus? ReturnStatus { get; set; }
}
