using MediatR;
using Pharmacy.Application.Dto;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Supplier.Queries.GetAll;

public class GetSuppliersQuery : Pagination, IRequest<Result<PaginationResponse<GetSuppliersResponse>>>
{
    public string? Name { get; set; }   
    public SupplierType? SupplierType { get; set; }
}
