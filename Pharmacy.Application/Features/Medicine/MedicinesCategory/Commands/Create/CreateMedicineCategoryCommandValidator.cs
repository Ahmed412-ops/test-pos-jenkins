using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Entities.Medicine;
using FluentValidation;
using Pharmacy.Application.Resources.Static;


namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Create;

public class CreateMedicineCategoryCommandValidator
 : BaseCommandValidator<
   CreateMedicineCategoryCommand,
   MedicineCategory>
{
    public CreateMedicineCategoryCommandValidator(IUnitOfWork context, bool checkName = true)
        : base(context, checkName)  
    {  
        RuleFor(x => x.ParentCategoryId)
            .MustAsync(async (parentId, cancellation) =>
            {
                if (parentId == null)
                    return true;
                var parentCategory = await context
                    .GetRepository<MedicineCategory>()
                    .IsExistsAsync(mc => mc.Id == parentId);
                return parentCategory;
            })
            .WithMessage(Messages.ParentCategoryDoesNotExist);
    }
}
