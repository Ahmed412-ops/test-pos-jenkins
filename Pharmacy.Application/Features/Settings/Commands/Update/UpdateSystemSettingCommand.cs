using MediatR;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Settings.Commands.Update;

public class UpdateSystemSettingCommand : IRequest<Result<string>>
{
    public Guid Id { get; set; }
    public string Value { get; set; } = default!;
}

