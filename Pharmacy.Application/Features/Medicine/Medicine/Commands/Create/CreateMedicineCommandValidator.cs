using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;

public class CreateMedicineCommandValidator
    : BaseCommandValidator<
    CreateMedicineCommand,
    Domain.Entities.Medicine.Medicine
    >
{
    public CreateMedicineCommandValidator(IUnitOfWork context, bool checkName = true)
        : base(context, checkName)
    {
        RuleFor(x => x.ManufacturerId)
            .MustExistManufacturer(context);

        RuleFor(x => x.CategoryId!.Value)
            .MustExistMedicineCategory(context)
            .When(x => x.CategoryId.HasValue);

        RuleFor(x => x.DosageFormId)
            .MustExistDosageForm(context);

        RuleForEach(x => x.EffectiveMaterials)
            .NotEmpty()
            .MustExistEffectiveMaterial(context);
   
        RuleForEach(x => x.CrossSellingRecommendations)
            .MustExistMedicine(context);
        
        RuleForEach(x => x.MedicineUnits)
            .ChildRules(medicineUnit =>
            {
                medicineUnit.RuleFor(x => x.UnitId)
                    .MustExistSellingUnit(context);

                medicineUnit.RuleFor(x => x.QuantityForCalcUnit)
                    .GreaterThan(0)
                    .When(x => x.CalcUnit)
                    .WithMessage(Messages.QuantityForCalcUnitMustBeGreaterThanZero);
            });           
    }
}
