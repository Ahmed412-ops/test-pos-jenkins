using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Commands.Create;

public class CreateDiseaseCommand : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public Guid DiseaseCategoryId { get; set; }
    public List<Guid> Symptoms { get; set; } = [];
}
