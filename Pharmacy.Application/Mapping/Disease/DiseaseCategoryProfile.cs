using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Disease.Category.Commands.Create;
using Pharmacy.Application.Features.Disease.Category.Commands.Update;
using Pharmacy.Application.Features.Disease.Category.Queries.GetAll;
using Pharmacy.Application.Features.Disease.Category.Queries.GetById;

namespace Pharmacy.Application.Mapping.Disease;

public class DiseaseCategoryProfile : MappingProfileBase
{
    public DiseaseCategoryProfile()
    {
        CreateMap<CreateDiseaseCategoryCommand, Domain.Entities.Disease.DiseaseCategory>();
        CreateMap<Domain.Entities.Disease.DiseaseCategory, GetDiseaseCategoriesResponse>();
        CreateMap<Domain.Entities.Disease.DiseaseCategory, DropDownQueryResponse>();

        CreateMap<Domain.Entities.Disease.DiseaseCategory, GetDiseaseCategoryResponse>()
            .ForMember(dest => dest.Diseases, 
                       opt => opt.MapFrom(src => src.Diseases));

        CreateMap<Domain.Entities.Disease.Disease, CommonQueryResponseBase>();
        CreateMap<UpdateDiseaseCategoryCommand, Domain.Entities.Disease.DiseaseCategory>();
    }
}
