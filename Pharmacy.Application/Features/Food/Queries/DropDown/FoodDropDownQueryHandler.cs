using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Queries.DropDown;

public class FoodDropDownQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<FoodDropDownQuery, Result<List<DropDownQueryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Food.Food> _foodRepo = unitOfWork.GetRepository<Domain.Entities.Food.Food>();

    public override async Task<Result<List<DropDownQueryResponse>>> Handle(
        FoodDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var foods = await _foodRepo.GetAllAsync(d => !d.Is_Deleted);
        var result = mapper.Map<List<DropDownQueryResponse>>(foods);
        return Result<List<DropDownQueryResponse>>.Success(result);
    }
}
