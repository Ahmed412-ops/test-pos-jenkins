using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Create;

public class CreateRecorderPointCommand : IRequest<Result<string>>
{
    public Guid MedicineId { get; set; }
    public Guid? PreferredSupplierId { get; set; }
    public decimal ReorderPoint { get; set; }
    public decimal RestockingQuantity { get; set; }
    public bool NotificationsEnabled { get; set; } = true;
}
