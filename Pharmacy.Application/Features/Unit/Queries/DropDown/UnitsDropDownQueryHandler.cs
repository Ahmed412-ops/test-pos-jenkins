using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Unit.Queries.DropDown;

public class UnitsDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<UnitsDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Unit.Unit> _unitRepo = unitOfWork.GetRepository<Domain.Entities.Unit.Unit>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        UnitsDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var units = await _unitRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(units);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
