using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Stock.Medication.Commands.Create;

public class CreateMedicationStockCommandValidator : AbstractValidator<CreateMedicationStockCommand>
{
    public CreateMedicationStockCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.MedicineId)
            .NotEmpty()
            .MustExistMedicine(unitOfWork);

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage(Messages.QuantityMustBeGreaterThanZero);

        RuleFor(x => x.ExpiryDate)
            .NotEmpty().WithMessage(Messages.ExpiryDateIsRequired);

        RuleFor(x => x.SellingPrice)
            .GreaterThan(0).WithMessage(Messages.SellingPriceMustBeGreaterThanZero);

    }
}