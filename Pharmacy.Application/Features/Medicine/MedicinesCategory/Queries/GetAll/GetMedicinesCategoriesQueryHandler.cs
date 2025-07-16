using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Medicine;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetAll;

public class GetMedicinesCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetMedicinesCategoriesQuery, Result<PaginationResponse<GetMedicineCategoriesResponse>>>
{
    private readonly IGenericRepository<MedicineCategory> _medicineCategoryRepo = unitOfWork.GetRepository<MedicineCategory>();

    public override async Task<Result<PaginationResponse<GetMedicineCategoriesResponse>>> Handle(
        GetMedicinesCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _medicineCategoryRepo.GetAllQueryableAsync(d=>!d.Is_Deleted,
                                Include: q => q.Include(mc => mc.ParentCategory!)
                                                .ThenInclude(mc => mc.ParentCategory!));

        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        if (request.CategoryLevel != null)
        {
            query = request.CategoryLevel switch
            {
                CategoryLevel.Main =>
                    query.Where(a => a.ParentCategoryId == null),
                CategoryLevel.Sub =>
                    query.Where(a => a.ParentCategoryId != null &&
                                    a.ParentCategory != null &&
                                    a.ParentCategory.ParentCategoryId == null),
                CategoryLevel.SubSub =>
                    query.Where(a => a.ParentCategoryId != null &&
                                    a.ParentCategory != null &&
                                    a.ParentCategory.ParentCategoryId != null),
                _ => query
            };
        }

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetMedicineCategoriesResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetMedicineCategoriesResponse>>.Success(
            new PaginationResponse<GetMedicineCategoriesResponse> { Data = response, Count = count }
        );
    }
}

