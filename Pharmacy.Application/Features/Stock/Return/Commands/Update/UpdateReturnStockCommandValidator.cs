using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Features.Stock.Return.Commands.Create;

namespace Pharmacy.Application.Features.Stock.Return.Commands.Update;

public class UpdateReturnStockCommandValidator : AbstractValidator<UpdateReturnStockCommand>
{
    public UpdateReturnStockCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id).NotEmpty();
        Include(new CreateMedicationReturnCommandValidator(unitOfWork));
    }
}
