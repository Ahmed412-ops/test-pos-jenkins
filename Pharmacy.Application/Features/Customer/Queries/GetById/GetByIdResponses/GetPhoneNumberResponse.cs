namespace Pharmacy.Application.Features.Customer.Queries.GetById.GetByIdResponses;

public class GetPhoneNumberResponse
{
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsWhatsApp { get; set; }
}
