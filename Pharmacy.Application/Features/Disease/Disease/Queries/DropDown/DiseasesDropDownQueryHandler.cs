using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Disease.Disease.Queries.DropDown;

public class DiseasesDropDownQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
 : BaseHandler<DiseasesDropDownQuery,Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Disease.Disease> _diseaseRepo = unitOfWork.GetRepository<Domain.Entities.Disease.Disease>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        DiseasesDropDownQuery request,
        CancellationToken cancellationToken
    )
    {
        var diseases = await _diseaseRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(diseases);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
