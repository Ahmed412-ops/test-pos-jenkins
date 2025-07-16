using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.DropDown;

public class MedicineCategoriesDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<MedicineCategoriesDropDownQuery, Result<List<MedicineCategoriesDropDownResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Medicine.MedicineCategory> _medicineCategoryRepo = unitOfWork.GetRepository<Domain.Entities.Medicine.MedicineCategory>();

    public override async Task<Result<List<MedicineCategoriesDropDownResponse>>> Handle(
        MedicineCategoriesDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var medicineCategories = await _medicineCategoryRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<MedicineCategoriesDropDownResponse>>(medicineCategories);
        return Result<List<MedicineCategoriesDropDownResponse>>.Success(result);
    }
}
