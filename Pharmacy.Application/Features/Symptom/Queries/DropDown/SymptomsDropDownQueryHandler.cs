using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Queries.DropDown;

public class SymptomsDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<SymptomsDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Symptoms.Symptom> _symptomRepo = unitOfWork.GetRepository<Domain.Entities.Symptoms.Symptom>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        SymptomsDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var symptoms = await _symptomRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(symptoms);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
