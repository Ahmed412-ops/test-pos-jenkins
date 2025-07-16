using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Food.Commands.Create;
using Pharmacy.Application.Features.Food.Commands.Update;
using Pharmacy.Application.Features.Food.Queries.GetAll;
using Pharmacy.Application.Features.Food.Queries.GetById;

namespace Pharmacy.Application.Mapping.Food;

public class FoodProfile : MappingProfileBase
{
    public FoodProfile()
    {
        CreateMap<CreateFoodCommand, Domain.Entities.Food.Food>();
        CreateMap<UpdateFoodCommand, Domain.Entities.Food.Food>();
        CreateMap<Domain.Entities.Food.Food, GetFoodResponse>();
        CreateMap<Domain.Entities.Food.Food, DropDownQueryResponse>();
        CreateMap<Domain.Entities.Food.Food, GetFoodByIdResponse>();
    }
}
