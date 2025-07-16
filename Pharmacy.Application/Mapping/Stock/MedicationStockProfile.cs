using Pharmacy.Application.Features.Stock.Medication.Commands.Create;
using Pharmacy.Application.Features.Stock.Medication.Commands.Update;
using Pharmacy.Application.Features.Stock.Medication.Queries.GetAll;
using Pharmacy.Application.Features.Stock.Medication.Queries.GetById;
using Pharmacy.Application.Features.Stock.Medication.Queries.GetMedicineLevel;

namespace Pharmacy.Application.Mapping.Stock;

public class MedicationStockProfile : MappingProfileBase
{
    public MedicationStockProfile()
    {
        CreateMap<CreateMedicationStockCommand, Domain.Entities.Stock.MedicationStock>()
            .ForMember(dest => dest.GeneratedBarcode, opt => opt.Ignore());

        CreateMap<Domain.Entities.Stock.MedicationStock, GetMedicationsResponse>()
            .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name));
        
        CreateMap<Domain.Entities.Stock.MedicationStock, GetMedicationResponse>()
            .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name));
        
        CreateMap<UpdateMedicationStockCommand, Domain.Entities.Stock.MedicationStock>()
            .ForMember(dest => dest.GeneratedBarcode, opt => opt.Ignore());
        
        CreateMap<Domain.Entities.Stock.MedicationStock, GetMedicineLevelResponse>()
            .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name));

    }
}
