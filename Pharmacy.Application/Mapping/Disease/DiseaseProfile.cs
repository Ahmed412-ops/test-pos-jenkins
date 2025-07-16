using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Disease.Disease.Commands.Create;
using Pharmacy.Application.Features.Disease.Disease.Commands.Update;
using Pharmacy.Application.Features.Disease.Disease.Queries.GetAll;
using Pharmacy.Application.Features.Disease.Disease.Queries.GetById;
using System.Linq;

namespace Pharmacy.Application.Mapping.Disease;

public class DiseaseProfile : MappingProfileBase
{
    public DiseaseProfile()
    {
        CreateMap<CreateDiseaseCommand, Domain.Entities.Disease.Disease>()
            .ForMember(dest => dest.Symptoms,
             opt => opt.MapFrom((src, dest) 
                => src.Symptoms.Select(SymptomId => new Domain.Entities.Disease.DiseaseSymptom
                    {
                        SymptomId = SymptomId,
                        DiseaseId = dest.Id
                    })));

        CreateMap<UpdateDiseaseCommand, Domain.Entities.Disease.Disease>()
            .IncludeBase<CreateDiseaseCommand, Domain.Entities.Disease.Disease>();

        CreateMap<Domain.Entities.Disease.Disease, GetDiseasesResponse>();
        CreateMap<Domain.Entities.Disease.Disease, DropDownQueryResponse>();
        CreateMap<Domain.Entities.Disease.Disease, GetDiseaseResponse>()
            .ForMember(dest => dest.Symptoms, 
                   opt => opt.MapFrom(src => src.Symptoms.Select(s => s.Symptom)))
            .ForMember(dest => dest.CategoryName, 
                   opt => opt.MapFrom(src => src.DiseaseCategory.Name))
            .ForMember(dest => dest.CategoryId, 
                   opt => opt.MapFrom(src => src.DiseaseCategoryId));
                   
        CreateMap<Domain.Entities.Symptoms.Symptom, CommonQueryResponseBase>();

    }
}
