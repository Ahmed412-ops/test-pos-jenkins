using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Create;

namespace Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Update;

public class UpdateRecorderPointCommandValidator : AbstractValidator<UpdateRecorderPointCommand>
{
    public UpdateRecorderPointCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id).NotEmpty();
        Include(new CreateRecorderPointCommandValidator(unitOfWork));
    }
}
