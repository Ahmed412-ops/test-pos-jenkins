using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Queries.GetNearExpiryMedicines;

public class GetNearExpiryMedicinesQuery : Pagination, IRequest<Result<PaginationResponse<GetNearExpiryMedicinesResponse>>>
{
    public string? MedicineName { get; set; }
    public DateOnly? ExpiryDateFrom { get; set; }
    public DateOnly? ExpiryDateTo { get; set; }
    public int MonthsThreshold { get; set; } = 3;
}
