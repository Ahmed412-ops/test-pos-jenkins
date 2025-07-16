namespace Pharmacy.Application.Services.Abstraction.GeneratorService;

public interface IPurchaseOrderNumberGenerator
{
    string GenerateUniquePurchaseOrderNumber(string orderName);
}
