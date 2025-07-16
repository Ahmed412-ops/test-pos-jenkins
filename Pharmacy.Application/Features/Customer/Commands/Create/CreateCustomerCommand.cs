using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommand : IBaseCommand, IRequest<Result<CreateCustomerResponse>>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool EnableContactOption { get; set; }
    public List<AddressDto> Addresses { get; set; } = new();
    public List<PhoneNumberDto> PhoneNumbers { get; set; } = new();
    public List<Guid> ChronicMedicineIds { get; set; } = new();
    public List<Guid> ChronicDiseaseIds { get; set; } = new();
}

public record AddressDto(string City, string? District, string StreetName, string? BuildingNumber, string? FloorNumber, string? ApartmentNumber, string? Landmark);
public record PhoneNumberDto(string Number, bool IsWhatsApp);
