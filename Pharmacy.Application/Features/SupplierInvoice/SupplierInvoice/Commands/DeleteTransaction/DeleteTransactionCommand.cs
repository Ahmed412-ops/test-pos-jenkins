using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SupplierInvoice.SupplierInvoice.Commands.DeleteTransaction;

public class DeleteTransactionCommand: IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
