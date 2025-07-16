using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Application.Features.Disease.Category.Queries.GetAll;

public class GetDiseaseCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetDiseaseCategoriesQuery, Result<PaginationResponse<GetDiseaseCategoriesResponse>>>
{
    private readonly IGenericRepository<DiseaseCategory> _diseaseCategoryRepo = unitOfWork.GetRepository<DiseaseCategory>();

    public override async Task<Result<PaginationResponse<GetDiseaseCategoriesResponse>>> Handle(
        GetDiseaseCategoriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _diseaseCategoryRepo.GetAllQueryableAsync(d => !d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetDiseaseCategoriesResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetDiseaseCategoriesResponse>>.Success(
            new PaginationResponse<GetDiseaseCategoriesResponse> { Data = response, Count = count }
        );
    }
}
