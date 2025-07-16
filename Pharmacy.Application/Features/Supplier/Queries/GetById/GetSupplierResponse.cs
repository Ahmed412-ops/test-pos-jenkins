using Pharmacy.Application.Dto.Common.Queries;
using Pharmacy.Application.Features.Supplier.Commands.Create;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Supplier.Queries.GetById;

public class GetSupplierResponse : CommonQueryResponseBase
{
    public SupplierType SupplierType { get; set; }
    public string? Address { get; set; }
    public string? PaymentTerms { get; set; }
    public List<ContactsDto> Contacts { get; set; } = [];   
}
