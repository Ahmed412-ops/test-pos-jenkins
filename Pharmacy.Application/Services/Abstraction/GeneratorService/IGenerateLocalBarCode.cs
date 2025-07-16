namespace Pharmacy.Application.Services.Abstraction.GeneratorService;

public interface IGenerateLocalBarCode
{
    string GenerateLocalBarcode(DateOnly expiryDate, decimal sellingPrice, int medicineIndex);
}
