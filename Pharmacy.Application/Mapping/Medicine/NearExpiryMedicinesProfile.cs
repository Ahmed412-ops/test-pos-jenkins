using Pharmacy.Application.Features.Stock.Medication.Queries.GetNearExpiryMedicines;
using Pharmacy.Application.Helper.Extensions;

namespace Pharmacy.Application.Mapping.Medicine
{
    public class NearExpiryMedicinesProfile : MappingProfileBase
    {
        public NearExpiryMedicinesProfile()
        {
            CreateMap<Domain.Entities.Stock.MedicationStock, GetNearExpiryMedicinesResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Medicine.Name))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => (int)src.Quantity))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.ExpiryDate.ToMonthYearString()))
                .ForMember(dest => dest.DaysUntilExpiry, opt => opt.MapFrom(src =>
                    (src.ExpiryDate.ToDateTime(TimeOnly.MinValue) - DateTime.Now.Date).Days));
        }
    }
}
