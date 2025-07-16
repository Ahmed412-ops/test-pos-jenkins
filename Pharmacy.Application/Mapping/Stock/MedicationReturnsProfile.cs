using Pharmacy.Application.Features.Stock.Return.Commands.Create;
using Pharmacy.Application.Features.Stock.Return.Commands.Update;
using Pharmacy.Application.Features.Stock.Return.Queries.GetAll;
using Pharmacy.Application.Features.Stock.Return.Queries.GetById;

namespace Pharmacy.Application.Mapping.Stock;

public class MedicationReturnsProfile : MappingProfileBase
{
    public MedicationReturnsProfile()
    {
        CreateMap<CreateMedicationReturnCommand, Domain.Entities.Stock.MedicationReturn>()
            .ForMember(dest => dest.ReturnItems, opt => opt.MapFrom(src => src.ReturnItems));

        CreateMap<MedicationReturnItemDto, Domain.Entities.Stock.MedicationReturnItem>();

        CreateMap<UpdateReturnStockCommand, Domain.Entities.Stock.MedicationReturn>()
            .IncludeBase<CreateMedicationReturnCommand, Domain.Entities.Stock.MedicationReturn>();

        CreateMap<Domain.Entities.Stock.MedicationReturn, GetMedicationReturnsResponse>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
            .ForMember(dest => dest.SupplierInvoiceNumber, opt => opt.MapFrom(src => src.SupplierInvoice!.InvoiceNumber));

        CreateMap<Domain.Entities.Stock.MedicationReturn, GetMedicationReturnResponse>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
            .ForMember(dest => dest.SupplierInvoiceNumber, opt => opt.MapFrom(src => src.SupplierInvoice!.InvoiceNumber))
            .ForMember(dest => dest.ReturnItems, opt => opt.MapFrom(src => src.ReturnItems));

        CreateMap<Domain.Entities.Stock.MedicationReturnItem, MedicationReturnItems>()
            .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.MedicineUnit.Medicine.Name))
            .ForMember(dest => dest.MedicineUnit, opt => opt.MapFrom(src => src.MedicineUnit.Unit.Name));

    }
}
