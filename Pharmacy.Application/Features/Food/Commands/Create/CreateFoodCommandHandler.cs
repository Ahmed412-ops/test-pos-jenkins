using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Food.Commands.Create;

public class CreateFoodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : BaseHandler<CreateFoodCommand, Result<string>>
{
    private readonly IGenericRepository<Domain.Entities.Food.Food> _foodRepository = unitOfWork.GetRepository<Domain.Entities.Food.Food>();

    public override async Task<Result<string>> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        var food = mapper.Map<Domain.Entities.Food.Food>(request);
        await _foodRepository.AddAsync(food);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Success(Messages.SuccessfullyCreated);
    }
}
