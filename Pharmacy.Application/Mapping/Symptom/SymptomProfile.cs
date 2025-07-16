using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Symptom.Commands.Create;
using Pharmacy.Application.Features.Symptom.Commands.Update;
using Pharmacy.Application.Features.Symptom.Queries.GetAll;
using Pharmacy.Application.Features.Symptom.Queries.GetById;

namespace Pharmacy.Application.Mapping.Symptom;

public class SymptomProfile : MappingProfileBase
{
    public SymptomProfile()
    {
        CreateMap<CreateSymptomCommand, Domain.Entities.Symptoms.Symptom>();
        CreateMap<UpdateSymptomCommand, Domain.Entities.Symptoms.Symptom>();
        CreateMap<Domain.Entities.Symptoms.Symptom, GetSymptomsResponse>();
        CreateMap<Domain.Entities.Symptoms.Symptom, GetSymptomResponse>();
        CreateMap<Domain.Entities.Symptoms.Symptom, DropDownQueryResponse>();
    }
}
