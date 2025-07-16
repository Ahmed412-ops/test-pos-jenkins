using MediatR;
using Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Create;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Update;

public class UpdateRecorderPointCommand : CreateRecorderPointCommand, IRequest<Result<string>>
{
    public Guid Id { get; set; }
}
