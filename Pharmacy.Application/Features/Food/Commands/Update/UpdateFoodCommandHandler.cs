using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Commands.Update;

public class UpdateFoodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<UpdateFoodCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Food.Food> _foodRepository = unitOfWork.GetRepository<Domain.Entities.Food.Food>();

    public override async Task<Result<string>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _foodRepository.FindAsync(
            s => s.Id == request.Id);

        if (food == null)
            return Result<string>.Fail(Messages.FoodNotFound);

        mapper.Map(request, food);

        await unitOfWork.SaveChangesAsync();

        return Result<string>.Success(Messages.SuccessfullyUpdated);
    }
}
