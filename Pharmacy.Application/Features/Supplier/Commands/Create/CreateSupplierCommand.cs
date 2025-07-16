using MediatR;
using Pharmacy.Application.Dto.Common.Commands;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Supplier.Commands.Create;

public class CreateSupplierCommand  : IBaseCommand, IRequest<Result<string>>
{
    public required string Name { get; set; }
    public SupplierType SupplierType { get; set; }
    public string? Address { get; set; }
    public string? PaymentTerms { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public List<ContactsDto> Contacts { get; set; } = [];
}

public class ContactsDto 
{
    public required string Name { get; set; }
    public string? Notes { get; set; }
    public string? Role { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Email { get; set; }
}