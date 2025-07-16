using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Shifts.Queries.GetById;

public class GetShiftQuery : IRequest<Result<GetShiftResponse>>
{
    public Guid Id { get; set; }
}
