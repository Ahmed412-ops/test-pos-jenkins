using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.Medicine.Queries.GetAll;

public class GetMedicinesQuery : Pagination, IRequest<Result<PaginationResponse<GetMedicinesResponse>>>
{
    public string? Name { get; set; }
    public string? Barcode { get; set; }  
}
