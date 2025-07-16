using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Application.Features.SaleScreen.Prescription.Commands.Create;

public class CreatePrescriptionItemDtoValidator : AbstractValidator<CreatePrescriptionItemDto>
{
    public CreatePrescriptionItemDtoValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(i => i.MedicationStockId).NotEmpty().MustExistMedicationStock(unitOfWork);
        

        RuleFor(i => i.Quantity)
            .GreaterThan(0)
            .WithMessage(Messages.QuantityMustBeGreaterThanZero)
            .MustAsync(
                async (dto, quantity, cancellation) =>
                {
                    var stock = await unitOfWork
                        .GetRepository<MedicationStock>()
                        .FindAsync(ms => ms.Id == dto.MedicationStockId);
                    return stock!.Quantity >= quantity;
                }
            )
            .WithMessage(Messages.QuantityExceedsAvailableStock);
        RuleFor(i => i.MedicineUnitId)
            .NotEmpty()
            .MustExistMedicineUnit(unitOfWork)
            .WithMessage(Messages.MedicineUnitNotFound);
    }
}
