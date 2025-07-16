using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Delete;

public class DeleteMedicineCategoryCommandHandler(IUnitOfWork unitOfWork)
 : BaseHandler<DeleteMedicineCategoryCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.MedicineCategory> _medicineCategoryRepository = unitOfWork.GetRepository<Domain.Entities.Medicine.MedicineCategory>();
    public override async Task<Result<bool>> Handle(DeleteMedicineCategoryCommand request, CancellationToken cancellationToken)
    {
        var medicineCategory = await _medicineCategoryRepository.FindAsync(
            f => f.Id == request.Id,
            Include: f => f.Include(f => f.SubCategories));
                            // .Include(f => f.MedicineCategoryTranslations));

        if (medicineCategory == null)
            return Result<bool>.Fail(Messages.NotFound);
        
        if (medicineCategory.SubCategories.Count != 0)
            return Result<bool>.Fail(Messages.RelationExists);
        
        // if (medicineCategory.Medicines.Count != 0)
        //     return Result<bool>.Fail(Messages.RelationExists);

        medicineCategory.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
