using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Stock.RecorderSettings.Commands.Create;

public class CreateRecorderPointCommandValidator : AbstractValidator<CreateRecorderPointCommand>
{
    public CreateRecorderPointCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.MedicineId)
            .NotEmpty()
            .MustExistMedicine(unitOfWork);
        
        RuleFor(x => x.ReorderPoint)
            .GreaterThan(0).WithMessage(Messages.ReorderPointMustBeGreaterThanZero);
        
        RuleFor(x => x.RestockingQuantity)
            .GreaterThan(0).WithMessage(Messages.QuantityMustBeGreaterThanZero);
    }
}
