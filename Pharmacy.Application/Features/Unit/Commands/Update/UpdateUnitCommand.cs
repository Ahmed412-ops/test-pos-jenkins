using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Commands.Update;

public class UpdateUnitCommand : IBaseUpdateCommand, IRequest<Result<string>>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }    
}
