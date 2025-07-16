using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Category.Commands.Delete;

public class DeleteDiseaseCategoryCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
