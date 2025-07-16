using MediatR;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Stock.Return.Commands.SetStatus;

public class SetStatusCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    public ReturnStatus ReturnStatus { get; set; }
}
