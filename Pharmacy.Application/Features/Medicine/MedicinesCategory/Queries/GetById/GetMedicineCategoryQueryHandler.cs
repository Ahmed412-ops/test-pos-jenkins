using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetById;

public class GetMedicineCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
 : BaseHandler<GetMedicineCategoryQuery, Result<GetMedicineCategoryResponse>>
{
    private readonly IGenericRepository<MedicineCategory> _medicineCategoryRepo = unitOfWork.GetRepository<MedicineCategory>();

    public override async Task<Result<GetMedicineCategoryResponse>> Handle(GetMedicineCategoryQuery request, CancellationToken cancellationToken)
    {
        var medicineCategory = await _medicineCategoryRepo.FindAsync(
            f => f.Id == request.Id,
            Include: q => q.Include(mc => mc.ParentCategory!)
                            .ThenInclude(mc => mc.ParentCategory!),
            asNoTracking: true);

        if (medicineCategory == null)
            return Result<GetMedicineCategoryResponse>.Fail(Messages.MedicineCategoryNotFound);

        var response = mapper.Map<GetMedicineCategoryResponse>(medicineCategory);

        return Result<GetMedicineCategoryResponse>.Success(response);
    }
}

