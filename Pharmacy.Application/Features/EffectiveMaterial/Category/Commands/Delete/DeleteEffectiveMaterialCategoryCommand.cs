using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Commands.Delete;

public class DeleteEffectiveMaterialCategoryCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
