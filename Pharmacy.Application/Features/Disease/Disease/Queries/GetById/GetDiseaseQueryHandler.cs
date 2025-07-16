using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Queries.GetById;

public class GetDiseaseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<GetDiseaseQuery, Result<GetDiseaseResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Disease.Disease> _diseaseRepo = unitOfWork.GetRepository<Domain.Entities.Disease.Disease>();


    public override async Task<Result<GetDiseaseResponse>> Handle(GetDiseaseQuery request, CancellationToken cancellationToken)
    {
        var disease = await _diseaseRepo.FindAsync(
            d => d.Id == request.Id,
            Include: d => d.Include(dc => dc.DiseaseCategory)
                   .Include(s => s.Symptoms)
                   .ThenInclude(s => s.Symptom!),
            asNoTracking: true);

        if (disease == null)
            return Result<GetDiseaseResponse>.Fail(Messages.DiseaseNotFound);

        var response = mapper.Map<GetDiseaseResponse>(disease);

        return Result<GetDiseaseResponse>.Success(response);
    }
}
