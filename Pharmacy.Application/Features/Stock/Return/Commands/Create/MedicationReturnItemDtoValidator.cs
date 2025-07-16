using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Stock.Return.Commands.Create;

public class MedicationReturnItemDtoValidator : AbstractValidator<MedicationReturnItemDto>
{
    public MedicationReturnItemDtoValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(i => i.MedicineUnitId)
            .NotEmpty()
            .MustExistMedicineUnit(unitOfWork);

        RuleFor(i => i.QuantityToReturn)
            .GreaterThan(0)
            .WithMessage(Messages.QuantityMustBeGreaterThanZero);

        RuleFor(x => x.ExpiryDate)
            .NotEmpty().WithMessage(Messages.ExpiryDateRequired);

        RuleFor(x => x.ReturnReason)
            .IsInEnum();

        RuleFor(x => x.Barcode)
            .NotEmpty().WithMessage(Messages.BarcodeRequired);
    }
}
