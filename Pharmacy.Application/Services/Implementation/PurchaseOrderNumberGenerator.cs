using Pharmacy.Application.Services.Abstraction.GeneratorService;

namespace Pharmacy.Application.Services.Implementation;

public class PurchaseOrderNumberGenerator : IPurchaseOrderNumberGenerator
{
    public string GenerateUniquePurchaseOrderNumber(string orderName)
    {
        string prefix = !string.IsNullOrWhiteSpace(orderName) 
            ? orderName[..Math.Min(orderName.Length, 3)].ToUpperInvariant() 
            : "PO";
            
        string uniqueSuffix = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
        return $"{prefix}-{uniqueSuffix}";
    }
}

