using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;
using Pharmacy.Application.Features.Medicine.Medicine.Queries.GetAll;
using Pharmacy.Application.Features.Medicine.Medicine.Queries.GetById;

namespace Pharmacy.Application.Mapping.Medicine;

public class MedicineQueryProfile : MappingProfileBase
{
    public MedicineQueryProfile()
    {
        CreateMap<Domain.Entities.Medicine.Medicine, GetMedicinesResponse>();
        CreateMap<Domain.Entities.Medicine.Medicine, DropDownQueryResponse>();
        CreateMap<Domain.Entities.Medicine.Medicine, GetMedicineResponse>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.MedicineCategory!.Name))
            .ForMember(dest => dest.ManufacturerName,
                opt => opt.MapFrom(src => src.Manufacturer.Name))
            .ForMember(dest => dest.DosageFormName,
                opt => opt.MapFrom(src => src.DosageForm.Name))
            .ForMember(dest => dest.EffectiveMaterials,
                opt => opt.MapFrom(src => src.EffectiveMaterials.Select(e => e.EffectiveMaterial)))
            .ForMember(dest => dest.CrossSellingRecommendations,
                opt => opt.MapFrom(src => src.CrossSellingRecommendations.Select(c => c.CrossSellingMedicine)))
            .ForMember(dest => dest.MedicineUnits,
                opt => opt.MapFrom(src => src.MedicineUnits.Select(u => new CreateMedicineUnitDto
                {
                    UnitId = u.UnitId,
                    CanBeSold = u.CanBeSold,
                    IsDefault = u.IsDefault,
                    CalcUnit = u.CalcUnit,
                    QuantityForCalcUnit = u.QuantityForCalcUnit
                })));

        CreateMap<Domain.Entities.EffectiveMaterial.EffectiveMaterial, CommonQueryResponseBase>();
        CreateMap<Domain.Entities.Unit.Unit, CommonQueryResponseBase>();
        CreateMap<Domain.Entities.Medicine.Medicine, CommonQueryResponseBase>();
        // CreateMap<Domain.Entities.Medicine.MedicineUnit, MedicineUnitDto>();

    }
}
