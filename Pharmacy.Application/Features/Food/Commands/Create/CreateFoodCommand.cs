using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Commands.Create;

public class CreateFoodCommand : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }    
}
