using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Queries.GetById;

public class GetFoodByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : BaseHandler<GetFoodByIdQuery, Result<GetFoodByIdResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Food.Food> _foodRepo = unitOfWork.GetRepository<Domain.Entities.Food.Food>();

    public override async Task<Result<GetFoodByIdResponse>> Handle(
        GetFoodByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var food = await _foodRepo.FindAsync(m => m.Id == request.Id);

        if (food == null)
            return Result<GetFoodByIdResponse>.Fail(Messages.FoodNotFound);

        var response = mapper.Map<GetFoodByIdResponse>(food);
        return Result<GetFoodByIdResponse>.Success(response);
    }
}
