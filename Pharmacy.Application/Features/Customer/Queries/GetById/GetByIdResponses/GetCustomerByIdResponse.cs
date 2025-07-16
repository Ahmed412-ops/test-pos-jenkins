using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;

public class GetCustomerByIdResponse : CommonQueryResponseBase
{
    public DateTime? DateOfBirth { get; set; }
    public int? Age { get; set; }
    public bool EnableContactOption { get; set; }
    public decimal AmountDue { get; set; }
    public decimal CashbackBalance { get; set; }
    public decimal CreditBalance { get; set; }
    public List<GetAddressResponse> Addresses { get; set; } = [];
    public List<GetPhoneNumberResponse> PhoneNumbers { get; set; } = [];
    public List<GetChronicMedicinesResponse> ChronicMedicines { get; set; } = [];
    public List<GetChronicDiseasesResponse> ChronicDiseases { get; set; } = [];
}
