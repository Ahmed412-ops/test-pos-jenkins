using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Commands.Delete;

public class DeleteFoodCommandHandler(IUnitOfWork unitOfWork)
    : BaseHandler<DeleteFoodCommand, Result<bool>>
{
    private readonly IGenericRepository<Domain.Entities.Food.Food> _foodRepository = unitOfWork.GetRepository<Domain.Entities.Food.Food>();
    public override async Task<Result<bool>> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _foodRepository.FindAsync(
            f => f.Id == request.Id,
            Include: f => f.Include(f => f.EffectiveMaterialFoods));

        if (food == null)
            return Result<bool>.Fail(Messages.NotFound);
        
        if (food.EffectiveMaterialFoods.Count != 0)
            return Result<bool>.Fail(Messages.RelationExists);

        food.Is_Deleted = true;
        
        int result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return Result<bool>.Fail(Messages.SomethingWentWrong);

        return Result<bool>.Success(true, Messages.DeletedSuccessfully);
    }
}
