using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.AdjustQuantity;

public class AdjustQuantityCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }  
    public decimal NewQuantity { get; set; } 
}
