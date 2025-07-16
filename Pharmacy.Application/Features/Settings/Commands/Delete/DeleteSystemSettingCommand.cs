using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Settings.Commands.Delete;

public record DeleteSystemSettingCommand(Guid Id) : IRequest<Result<bool>>;
