using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetAll;

public class GetMedicationsQuery : Pagination, IRequest<Result<PaginationResponse<GetMedicationsResponse>>>
{
    public string? MedicineName { get; set; }
    public string? Barcode { get; set; }
    public bool? IsConfirmed { get; set; }
}
