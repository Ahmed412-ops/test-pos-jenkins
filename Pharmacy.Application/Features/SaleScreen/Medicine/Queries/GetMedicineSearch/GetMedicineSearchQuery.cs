using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetMedicineSearch;

public class GetMedicineSearchQuery : Pagination, IRequest<Result<PaginationResponse<GetMedicineSearchResponse>>>
{
    public string? MedicineName { get; set; }
}

