using Pharmacy.Application.Features.Medicine.Medicine.Commands.Create;
using Pharmacy.Application.Features.Medicine.Medicine.Commands.Update;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Application.Mapping.Medicine;

public class MedicineCommandProfile : MappingProfileBase
{
    public MedicineCommandProfile()
    {
        CreateMap<CreateMedicineCommand, Domain.Entities.Medicine.Medicine>()
            .ForMember(dest => dest.MedicineUnits,
                opt => opt.MapFrom((src, dest) 
                    => src.MedicineUnits.Select(sellingUnitId => new MedicineUnit
                        {
                            UnitId = sellingUnitId.UnitId,
                            CanBeSold = sellingUnitId.CanBeSold,
                            IsDefault = sellingUnitId.IsDefault,
                            CalcUnit = sellingUnitId.CalcUnit,
                            QuantityForCalcUnit = sellingUnitId.QuantityForCalcUnit,
                            MedicineId = dest.Id
                        })))
            .ForMember(dest => dest.CrossSellingRecommendations,
                opt => opt.MapFrom((src, dest) 
                    => src.CrossSellingRecommendations.Select(crossSellingId => new MedicineCrossSelling
                        {
                            CrossSellingMedicineId = crossSellingId,
                            MedicineId = dest.Id
                        })))
            .ForMember(dest => dest.EffectiveMaterials,
                opt => opt.MapFrom((src, dest) 
                    => src.EffectiveMaterials.Select(effectiveMaterialId => new MedicineEffectiveMaterial
                        {
                            EffectiveMaterialId = effectiveMaterialId,
                            MedicineId = dest.Id
                        })));
        
        CreateMap<UpdateMedicineCommand, Domain.Entities.Medicine.Medicine>()
            .IncludeBase<CreateMedicineCommand, Domain.Entities.Medicine.Medicine>();
    }
}
