using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Update;

public class UpdateMedicineCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateMedicineCategoryCommand, Result<string>>
{
    private readonly IGenericRepository<MedicineCategory> _medicineCategoryRepository = unitOfWork.GetRepository<MedicineCategory>();


    public override async Task<Result<string>> Handle(UpdateMedicineCategoryCommand request, CancellationToken cancellationToken)
    {
        var medicineCategory = await _medicineCategoryRepository.FindAsync(
            mc => mc.Id == request.Id);

        if (medicineCategory == null)
            return Result<string>.Fail(Messages.MedicineCategoryNotFound);

        mapper.Map(request, medicineCategory);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}

