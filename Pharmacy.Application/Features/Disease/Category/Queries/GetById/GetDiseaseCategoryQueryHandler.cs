using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Disease;

namespace Pharmacy.Application.Features.Disease.Category.Queries.GetById;

public class GetDiseaseCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<GetDiseaseCategoryQuery, Result<GetDiseaseCategoryResponse>>
{
    private readonly IGenericRepository<DiseaseCategory> _diseaseCategoryRepo = unitOfWork.GetRepository<DiseaseCategory>();


    public override async Task<Result<GetDiseaseCategoryResponse>> Handle(GetDiseaseCategoryQuery request, CancellationToken cancellationToken)
    {
        var diseaseCategory = await _diseaseCategoryRepo.FindAsync(
            d => d.Id == request.Id,
            Include: d=> d.Include(dc => dc.Diseases),
            asNoTracking: true);

        if (diseaseCategory == null)
            return Result<GetDiseaseCategoryResponse>.Fail(Messages.DiseaseCategoryNotFound);

        var response = mapper.Map<GetDiseaseCategoryResponse>(diseaseCategory);

        return Result<GetDiseaseCategoryResponse>.Success(response);
    }
}
