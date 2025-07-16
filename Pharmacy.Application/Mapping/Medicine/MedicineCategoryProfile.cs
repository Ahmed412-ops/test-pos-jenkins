using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Create;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Commands.Update;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.DropDown;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetAll;
using Pharmacy.Application.Features.Medicine.MedicinesCategory.Queries.GetById;

namespace Pharmacy.Application.Mapping.Medicine;

public class MedicineCategoryProfile : MappingProfileBase
{
    public MedicineCategoryProfile()
    {
        CreateMap<CreateMedicineCategoryCommand, Domain.Entities.Medicine.MedicineCategory>();
        CreateMap<UpdateMedicineCategoryCommand, Domain.Entities.Medicine.MedicineCategory>();
        CreateMap<Domain.Entities.Medicine.MedicineCategory, GetMedicineCategoriesResponse>();
        CreateMap<Domain.Entities.Medicine.MedicineCategory, MedicineCategoriesDropDownResponse>();
        CreateMap<Domain.Entities.Medicine.MedicineCategory, GetMedicineCategoryResponse>();
    }
}
