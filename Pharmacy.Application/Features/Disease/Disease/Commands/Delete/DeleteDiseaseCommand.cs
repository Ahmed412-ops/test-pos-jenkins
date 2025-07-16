using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Commands.Delete;

public class DeleteDiseaseCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
