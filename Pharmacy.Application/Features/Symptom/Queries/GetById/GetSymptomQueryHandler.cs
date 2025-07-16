using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Queries.GetById;

public class GetSymptomQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetSymptomQuery, Result<GetSymptomResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Symptoms.Symptom> _symptomRepo = unitOfWork.GetRepository<Domain.Entities.Symptoms.Symptom>();

    public override async Task<Result<GetSymptomResponse>> Handle(
        GetSymptomQuery request,
        CancellationToken cancellationToken
    )
    {
        var symptom = await _symptomRepo.FindAsync(s => s.Id == request.Id);

        if (symptom == null)
            return Result<GetSymptomResponse>.Fail(Messages.SymptomNotFound);

        var response = mapper.Map<GetSymptomResponse>(symptom);
        return Result<GetSymptomResponse>.Success(response);
    }
}
