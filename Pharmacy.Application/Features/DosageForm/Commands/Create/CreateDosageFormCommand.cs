using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.DosageForm.Commands.Create;

public class CreateDosageFormCommand : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get; set; }
    public bool IsLiquidMedicine { get; set; }
    public bool AffectsDrugInteractions { get; set; } 
    public string? Description { get; set; }
    public string? Notes { get; set; }
}
