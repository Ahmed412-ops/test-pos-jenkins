using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Return.Commands.Create;

public class CreateReturnCommand : IRequest<Result<CreateReturnResponse>>
{
    public Guid PrescriptionId { get; set; }
    public Guid ShiftWalletId { get; set; }
    public string? Notes { get; set; }
    public List<ReturnItemDto> Items { get; set; } = [];
}

public class ReturnItemDto
{
    public Guid PrescriptionItemId { get; set; }
    public int QuantityReturned { get; set; }
    public string? Reason { get; set; }
    public bool IsDamaged { get; set; } = false;
}
