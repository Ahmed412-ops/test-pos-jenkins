using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Supplier.Commands.Delete;

public class DeleteSupplierCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
