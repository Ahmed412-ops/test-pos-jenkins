using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Create;
using Pharmacy.Application.Features.EffectiveMaterial.EffectiveMaterial.Commands.Update;
using Pharmacy.Domain.Entities.EffectiveMaterial;
using Pharmacy.Domain.Entities.Medicine;

namespace Pharmacy.Application.Mapping.EffectiveMaterial;

public class EffectiveMaterialCommandProfile : MappingProfileBase
{
    public EffectiveMaterialCommandProfile()
    {
        CreateMap<CreateEffectiveMaterialCommand, Domain.Entities.EffectiveMaterial.EffectiveMaterial>()
            .ForMember(dest => dest.CommonUses,
                opt => opt.MapFrom((src, dest) 
                    => src.CommonUses.Select(commonUseId => new EffectiveMaterialCommonUse
                        {
                            UseId = commonUseId,
                            EffectiveMaterialId = dest.Id
                        })))
            .ForMember(dest => dest.OffLabelUses,
                opt => opt.MapFrom((src, dest) 
                    => src.OffLabelUses.Select(offLabelUseId => new EffectiveMaterialOffLabelUse
                        {
                            UseId = offLabelUseId,
                            EffectiveMaterialId = dest.Id
                        })))
            .ForMember(dest => dest.SideEffects,
                opt => opt.MapFrom((src, dest) 
                    => src.SideEffects.Select(sideEffectId => new EffectiveMaterialSideEffect
                        {
                            SideEffectId = sideEffectId.SideEffectId,
                            EffectiveMaterialId = dest.Id,
                            Probability = sideEffectId.Probability,
                            IsMajor = sideEffectId.IsMajor
                        })))
            .ForMember(dest => dest.FoodInteractions,
                opt => opt.MapFrom((src, dest) 
                    => src.FoodInteractions.Select(foodInteractionId => new EffectiveMaterialFood
                        {
                            FoodId = foodInteractionId,
                            EffectiveMaterialId = dest.Id
                        })))
            .ForMember(dest => dest.DiseaseInteraction,
                opt => opt.MapFrom((src, dest) 
                    => src.DiseaseInteraction.Select(diseaseInteractionId => new EffectiveMaterialDisease
                    {
                            DiseaseId = diseaseInteractionId,
                            EffectiveMaterialId = dest.Id
                        })))
            .ForMember(dest => dest.EM_CrossSelling,
                opt => opt.MapFrom((src, dest) 
                    => src.CrossSelling.Select(crossSellingId => new EffectiveMaterialCrossSelling
                    {
                            CrossSellingMaterialId = crossSellingId,
                            EffectiveMaterialId = dest.Id
                        })))
            .ForMember(dest => dest.EM_DrugInteractions,
                opt => opt.MapFrom((src, dest) 
                    => src.DrugInteraction.Select(drugInteractionId => new EffectiveMaterialDrugInteraction
                    {
                            InteractingMaterialId = drugInteractionId,
                            EffectiveMaterialId = dest.Id
                        })))
            .ForMember(dest => dest.MedicinesCrossSelling,
                opt => opt.MapFrom((src, dest) 
                    => src.MedicinesCrossSelling.Select(medicineId => new MedicineEffectiveMaterialCrossSelling
                    {
                            MedicineId = medicineId,
                            EffectiveMaterialId = dest.Id
                        })))
            .ForMember(dest => dest.MedicinesDrugInteractions,
                opt => opt.MapFrom((src, dest) 
                    => src.MedicinesDrugInteractions.Select(medicineId => new MedicineEffectiveMaterialInteraction
                    {
                            MedicineId = medicineId,
                            EffectiveMaterialId = dest.Id
                        })));

        CreateMap<UpdateEffectiveMaterialCommand, Domain.Entities.EffectiveMaterial.EffectiveMaterial>()
            .IncludeBase<CreateEffectiveMaterialCommand, Domain.Entities.EffectiveMaterial.EffectiveMaterial>();
    }
}
