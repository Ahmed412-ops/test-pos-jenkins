using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Commands.Delete;

public class DeleteManufacturerCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}
