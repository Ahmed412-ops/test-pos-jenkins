using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities.Settings;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Settings.Commands.Update;

public class UpdateSystemSettingCommandValidator : AbstractValidator<UpdateSystemSettingCommand>
{
    private readonly IGenericRepository<SystemSetting> _repo;

    public UpdateSystemSettingCommandValidator(IUnitOfWork unitOfWork)
    {
        _repo = unitOfWork.GetRepository<SystemSetting>();

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(Messages.IdIsRequired);

        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage(Messages.ValueIsRequired);

        RuleFor(x => x)
            .CustomAsync(async (command, context, cancellationToken) =>
            {
                // Fetch the setting once
                var setting = await _repo.FindAsync(a => a.Id == command.Id);
                if (setting == null)
                {
                    context.AddFailure(nameof(command.Id), Messages.NotFound);
                    return;
                }

                // Validate the value based on the SettingType
                if (!IsValidForType(command.Value, setting.Type, out var errorMessage))
                {
                    context.AddFailure(nameof(command.Value), errorMessage);
                }
            });
    }

    private bool IsValidForType(string value, SettingType type, out string errorMessage)
    {
        errorMessage = string.Empty;

        switch (type)
        {
            case SettingType.Boolean:
                if (!bool.TryParse(value, out _))
                {
                    errorMessage = Messages.ExpectedBoolean;
                    return false;
                }
                break;

            case SettingType.Decimal:
                if (!decimal.TryParse(value, out _))
                {
                    errorMessage = Messages.ExpectedDecimal;
                    return false;
                }
                break;

            case SettingType.Integer:
                if (!int.TryParse(value, out _))
                {
                    errorMessage = Messages.ExpectedInteger;
                    return false;
                }
                break;

            case SettingType.String:
                // Strings are always valid
                return true;

            default:
                errorMessage = Messages.UnsupportedSettingType;
                return false;
        }

        return true;
    }
}
