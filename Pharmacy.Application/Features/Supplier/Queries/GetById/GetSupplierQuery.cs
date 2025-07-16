using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Supplier.Queries.GetById;

public class GetSupplierQuery : IRequest<Result<GetSupplierResponse>>
{
    public Guid Id { get; set; }    
}
