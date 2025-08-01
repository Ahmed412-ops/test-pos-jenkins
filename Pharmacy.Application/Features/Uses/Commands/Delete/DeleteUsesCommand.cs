using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Commands.Delete;

public class DeleteUsesCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
