using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Uses.Queries.DropDown;

public class UsesDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<UsesDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Uses.Use> _useRepo = unitOfWork.GetRepository<Domain.Entities.Uses.Use>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        UsesDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var uses = await _useRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(uses);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
