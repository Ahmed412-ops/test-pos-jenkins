using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Symptom.Queries.GetAll;

public class GetSymptomsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetSymptomsQuery, Result<PaginationResponse<GetSymptomsResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Symptoms.Symptom> _symptomRepo = unitOfWork.GetRepository<Domain.Entities.Symptoms.Symptom>();

    public override async Task<Result<PaginationResponse<GetSymptomsResponse>>> Handle(
        GetSymptomsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = await _symptomRepo.GetAllQueryableAsync(d=>!d.Is_Deleted);
        if(!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(a => a.Name.Contains(request.Name));

        var count = await query.CountAsync(cancellationToken);

        var response = query
            .Select(a => mapper.Map<GetSymptomsResponse>(a))
            .Paginate(request)
            .ToList();

        return Result<PaginationResponse<GetSymptomsResponse>>.Success(
            new PaginationResponse<GetSymptomsResponse> { Data = response, Count = count }
        );
    }
}
