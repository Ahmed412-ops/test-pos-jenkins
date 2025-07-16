using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Queries.GetAll;

public class GetDosageFormsQuery : Pagination, IRequest<Result<PaginationResponse<GetDosageFormsResponse>>>
{
    public string? Name { get; set; }
}
