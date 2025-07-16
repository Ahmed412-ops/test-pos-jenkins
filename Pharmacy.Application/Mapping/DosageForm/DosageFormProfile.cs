using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.DosageForm.Commands.Create;
using Pharmacy.Application.Features.DosageForm.Commands.Update;
using Pharmacy.Application.Features.DosageForm.Queries.GetAll;
using Pharmacy.Application.Features.DosageForm.Queries.GetById;

namespace Pharmacy.Application.Mapping.DosageForm;

public class DosageFormProfile : MappingProfileBase
{
    public DosageFormProfile()
    {
        CreateMap<CreateDosageFormCommand, Domain.Entities.DosageForm.DosageForm>();
        CreateMap<UpdateDosageFormCommand, Domain.Entities.DosageForm.DosageForm>();
        CreateMap<Domain.Entities.DosageForm.DosageForm, GetDosageFormsResponse>();
        CreateMap<Domain.Entities.DosageForm.DosageForm, DropDownQueryResponse>();
        CreateMap<Domain.Entities.DosageForm.DosageForm, GetDosageFormResponse>();
    }
}
