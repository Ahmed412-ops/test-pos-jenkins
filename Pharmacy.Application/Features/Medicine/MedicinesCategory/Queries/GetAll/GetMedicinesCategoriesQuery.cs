using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetAll;

public class GetMedicinesCategoriesQuery : Pagination, IRequest<Result<PaginationResponse<GetMedicineCategoriesResponse>>>
{
    public string? Name { get; set; }
    public CategoryLevel? CategoryLevel { get; set; }
}
