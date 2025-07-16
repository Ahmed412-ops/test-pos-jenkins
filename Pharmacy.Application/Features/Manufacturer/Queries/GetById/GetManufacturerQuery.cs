using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Manufacturer.Queries.GetById;

public class GetManufacturerQuery : IRequest<Result<GetManufacturerResponse>>
{
    public Guid Id { get; set; }
}
