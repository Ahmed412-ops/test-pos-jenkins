using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Create;

public class CreateMedicineCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateMedicineCategoryCommand, Result<string>>
{
    private readonly IGenericRepository<MedicineCategory> _medicineCategoryRepository = unitOfWork.GetRepository<MedicineCategory>();


    public override async Task<Result<string>> Handle(CreateMedicineCategoryCommand request, CancellationToken cancellationToken)
    {
        var medicineCategory = mapper.Map<MedicineCategory>(request);

        await _medicineCategoryRepository.AddAsync(medicineCategory);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
