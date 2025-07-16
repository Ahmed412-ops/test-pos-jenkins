using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.GetAll;

public class GetPrescriptionsQuery
    : Pagination,
        IRequest<Result<PaginationResponse<GetPrescriptionsResponse>>>
{
    public int? InvoiceNumber { get; set; }
    public string? CustomerNameOrPhone { get; set; }
    public DateTime? PrescriptionStartDate { get; set; }
    public DateTime? PrescriptionEndDate { get; set; }
}
