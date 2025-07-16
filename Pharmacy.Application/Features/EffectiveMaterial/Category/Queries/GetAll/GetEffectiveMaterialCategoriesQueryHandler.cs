using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.EffectiveMaterial;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetAll;

public class GetEffectiveMaterialCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
   : BaseHandler<GetEffectiveMaterialCategoriesQuery, Result<PaginationResponse<GetEffectiveMaterialCategoriesResponse>>>
{
    private readonly IGenericRepository<EffectiveMaterialCategory> _effectiveMaterialCategoryRepo = unitOfWork.GetRepository<EffectiveMaterialCategory>();

    public override async Task<Result<PaginationResponse<GetEffectiveMaterialCategoriesResponse>>> Handle(
        GetEffectiveMaterialCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _effectiveMaterialCategoryRepo.GetAllQueryableAsync(d=>!d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetEffectiveMaterialCategoriesResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetEffectiveMaterialCategoriesResponse>>.Success(
            new PaginationResponse<GetEffectiveMaterialCategoriesResponse> { Data = response, Count = count }
        );
    }
}

