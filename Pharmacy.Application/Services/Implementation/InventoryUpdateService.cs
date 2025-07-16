using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Services.Abstraction.GeneratorService;
using Pharmacy.Application.Services.Abstraction.InventoryUpdateService;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Application.Services.Implementation;

public class InventoryUpdateService(
    IUnitOfWork unitOfWork,
    IGenerateLocalBarCode generateLocalBarCode
) : IInventoryUpdateService
{
    private readonly IGenericRepository<MedicationStock> _stockRepo =
        unitOfWork.GetRepository<MedicationStock>();

    public async Task UpdateInventoryFromReviewedInvoiceAsync(
        Domain.Entities.SupplierInvoice.SupplierInvoice invoice,
        CancellationToken cancellationToken
    )
    {
        var newStockList = new List<MedicationStock>();

        foreach (var item in invoice.InvoiceItems)
        {
            var medicine = item.MedicineUnit.Medicine;
            var LocalBarcode = generateLocalBarCode.GenerateLocalBarcode(
                item.ExpiryDate,
                item.PublicSellingPrice,
                medicine.Index
            );

            var existingMedicationStock = await _stockRepo.FindAsync(x =>
                x.GeneratedBarcode == LocalBarcode
            );

            decimal finalQuantity = item.ReviewedQuantity ?? item.Quantity;
            if (existingMedicationStock != null)
            {
                existingMedicationStock.Quantity += finalQuantity;
            }
            else
            {
                newStockList.Add(
                    new MedicationStock
                    {
                        MedicineId = (Guid)item.MedicineUnit.MedicineId!,
                        Quantity = finalQuantity,
                        ExpiryDate = item.ExpiryDate,
                        PurchasePrice = item.SupplierPurchasePrice,
                        SellingPrice = item.PublicSellingPrice,
                        GeneratedBarcode = LocalBarcode,
                    }
                );
            }
        }

        if (newStockList.Count != 0)
            await _stockRepo.AddRangeAsync(newStockList);

        await unitOfWork.SaveChangesAsync();
    }
}
