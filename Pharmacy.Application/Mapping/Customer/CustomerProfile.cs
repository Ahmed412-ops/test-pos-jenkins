using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Customer.Commands.Create;
using Pharmacy.Application.Features.Customer.Commands.Update;
using Pharmacy.Application.Features.Customer.Queries.GetAll;
using Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;
using Pharmacy.Application.Features.Customer.Queries.SearchCustomer;
using Pharmacy.Domain.Entities.Customers;

namespace Pharmacy.Application.Mapping.Customer;

public class CustomerProfile : MappingProfileBase
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerCommand, Domain.Entities.Customers.Customer>()
            .ForMember(
                dest => dest.CustomerChronicMedicines,
                opt =>
                    opt.MapFrom(src =>
                        src.ChronicMedicineIds.Select(medicineId => new CustomerChronicMedicine
                        {
                            MedicineId = medicineId,
                        })
                    )
            )
            .ForMember(
                dest => dest.CustomerChronicDiseases,
                opt =>
                    opt.MapFrom(src =>
                        src.ChronicDiseaseIds.Select(diseaseId => new CustomerChronicDisease
                        {
                            DiseaseId = diseaseId,
                        })
                    )
            );

        CreateMap<UpdateCustomerCommand, Domain.Entities.Customers.Customer>()
            .IncludeBase<CreateCustomerCommand, Domain.Entities.Customers.Customer>();

        // Address Mapping
        CreateMap<AddressDto, Address>();
        // Phone Number Mapping
        CreateMap<PhoneNumberDto, PhoneNumber>();
        // Mapping for fetching customers
        CreateMap<Domain.Entities.Customers.Customer, GetCustomersResponse>();
        CreateMap<Domain.Entities.Customers.Customer, DropDownQueryResponse>();
        CreateMap<Domain.Entities.Customers.Customer, GetCustomerByIdResponse>()
            .ForMember(
                dest => dest.Age,
                opt =>
                    opt.MapFrom(src =>
                        src.DateOfBirth.HasValue
                            ? (int?)((DateTime.Today - src.DateOfBirth.Value).TotalDays / 365.25)
                            : null
                    )
            )
            .ForMember(
                dest => dest.ChronicMedicines,
                opt =>
                    opt.MapFrom(src =>
                        src.CustomerChronicMedicines.Select(cm => new GetChronicMedicinesResponse
                        {
                            Id = cm.MedicineId,
                            Name = cm.Medicine.Name,
                        })
                    )
            )
            .ForMember(
                dest => dest.ChronicDiseases,
                opt =>
                    opt.MapFrom(src =>
                        src.CustomerChronicDiseases.Select(cd => new GetChronicDiseasesResponse
                        {
                            Id = cd.DiseaseId,
                            Name = cd.Disease.Name,
                        })
                    )
            );

        CreateMap<Address, GetAddressResponse>();

        CreateMap<PhoneNumber, GetPhoneNumberResponse>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Number));
    }
}
