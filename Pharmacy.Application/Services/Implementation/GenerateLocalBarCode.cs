using Pharmacy.Application.Services.Abstraction.GeneratorService;

namespace Pharmacy.Application.Services.Implementation;

public class GenerateLocalBarCode : IGenerateLocalBarCode
{
    public string GenerateLocalBarcode(DateOnly expiryDate, decimal sellingPrice, int medicineIndex)
    {
        // Convert medicine index to a string and pad it with zeros to the left
        string medicineIndexPart = medicineIndex.ToString().PadLeft(5, '0');

        // Extract last two digits of the expiry year
        string yearPart = expiryDate.ToString("yy");  
        
        // Get expiry month in two digits
        string monthPart = expiryDate.ToString("MM");  
        
        // Convert selling price to a string and take the first four digits.
        // You may need to handle rounding/padding if sellingPrice doesn't have four digits.
        string pricePart = ((int)Math.Floor(sellingPrice)).ToString().PadLeft(4, '0').Substring(0, 4);
        
        return $"{medicineIndexPart}{yearPart}{monthPart}{pricePart}";
    }
}
