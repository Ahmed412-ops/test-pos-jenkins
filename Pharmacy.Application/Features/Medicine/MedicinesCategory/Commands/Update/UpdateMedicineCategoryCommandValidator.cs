using FluentValidation;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Create;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Update;

public class UpdateMedicineCategoryCommandValidator : UpdateBaseCommandValidator<UpdateMedicineCategoryCommand, MedicineCategory>
{
    public UpdateMedicineCategoryCommandValidator(IUnitOfWork context)
        : base(context) 
    {
        Include(new CreateMedicineCategoryCommandValidator(context, false));  
        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                // If no parent is provided, there's no risk of a circular reference.
                if (command.ParentCategoryId == null)
                    return true;

                // Prevent setting itself as its own parent.
                if (command.ParentCategoryId == command.Id)
                    return false;

                // Traverse the parent chain to check for circular references.
                var parentExists = await context.GetRepository<MedicineCategory>()
                    .IsExistsAsync(mc => mc.Id == command.ParentCategoryId);
                
                if (!parentExists)
                    return false;

                var currentParent = await context.GetRepository<MedicineCategory>()
                    .FindAsync(mc => mc.Id == command.ParentCategoryId);

                while (currentParent != null)
                {
                    // If any ancestor is the category itself, it's a circular reference.
                    if (currentParent.Id == command.Id)
                        return false;

                    // Stop if we reach a main category.
                    if (currentParent.ParentCategoryId == null)
                        break;

                    currentParent = await context.GetRepository<MedicineCategory>()
                        .FindAsync(mc => mc.Id == currentParent.ParentCategoryId);
                }
                return true;
            })
            .WithMessage(Messages.CircularReferenceDetected);
         
    }
}
