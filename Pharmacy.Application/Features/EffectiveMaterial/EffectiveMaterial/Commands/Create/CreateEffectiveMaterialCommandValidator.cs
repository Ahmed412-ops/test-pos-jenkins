using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Create;

public class CreateEffectiveMaterialCommandValidator
    : BaseCommandValidator<
        CreateEffectiveMaterialCommand,
        Domain.Entities.EffectiveMaterial.EffectiveMaterial
    >
{
    public CreateEffectiveMaterialCommandValidator(IUnitOfWork context, bool checkName = true)
        : base(context, checkName) 
        {
            RuleFor(x => x.CategoryId)
                .MustExistEffectiveMaterialCategory(context);

            RuleForEach(x => x.SideEffects)
                .ChildRules(sideEffect =>
                {
                    sideEffect.RuleFor(y => y.SideEffectId)
                        .MustExistSideEffect(context);
                });

            RuleForEach(x => x.CommonUses)
                .MustExistUse(context);
            
            RuleForEach(x => x.OffLabelUses)
                .MustExistUse(context);

            RuleForEach(x => x.FoodInteractions)
                .MustExistFoodInteraction(context);
            
            RuleForEach(x => x.DiseaseInteraction)
                .MustExistDiseaseInteraction(context);

            RuleForEach(x => x.CrossSelling)
                .MustExistEffectiveMaterial(context);
            
            RuleForEach(x => x.DrugInteraction)
                .MustExistEffectiveMaterial(context);
            
            RuleForEach(x => x.MedicinesDrugInteractions)
                .MustExistMedicine(context);
            
            RuleForEach(x => x.MedicinesCrossSelling)
                .MustExistMedicine(context);
            

            RuleFor(x => x.DrugInteraction)
                .Must(list => list.Distinct().Count() == list.Count)
                .WithMessage(Messages.DrugInteractionDuplicate);
            
            RuleFor(x => x)
                .Must(x => !x.DrugInteraction.Any(id => x.CrossSelling.Contains(id)))
                .WithMessage(Messages.DrugInteractionCrossSellingConflict);
            
            RuleFor(x => x)
                .Must(x => !x.DrugInteraction.Any(id => x.MedicinesDrugInteractions.Contains(id)))
                .WithMessage(Messages.DrugInteractionMedicineConflict);
        }
}
