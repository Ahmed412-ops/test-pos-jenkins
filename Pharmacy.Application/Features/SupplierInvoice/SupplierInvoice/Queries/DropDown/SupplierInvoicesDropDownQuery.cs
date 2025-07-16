using MediatR;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Queries.DropDown;

public class SupplierInvoicesDropDownQuery : IRequest<Result<List<DropDownQueryResponse>>>
{
    
}
