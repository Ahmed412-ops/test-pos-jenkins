using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Supplier.Queries.GetAll;

public class GetSuppliersResponse : CommonQueryResponseBase
{
    public required string SupplierType { get; set; }
   
}
