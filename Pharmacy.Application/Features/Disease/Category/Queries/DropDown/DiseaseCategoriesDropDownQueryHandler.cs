using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Application.Features.Disease.Category.Queries.DropDown;

public class DiseaseCategoriesDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<DiseaseCategoriesDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<DiseaseCategory> _diseaseCategoryRepo = unitOfWork.GetRepository<DiseaseCategory>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        DiseaseCategoriesDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var diseaseCategories = await _diseaseCategoryRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(diseaseCategories);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
