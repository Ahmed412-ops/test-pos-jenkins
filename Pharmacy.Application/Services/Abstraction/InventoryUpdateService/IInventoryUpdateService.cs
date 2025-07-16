using Pharmacy.Domain.Entities.SupplierInvoice;

namespace Pharmacy.Application.Services.Abstraction.InventoryUpdateService;

public interface IInventoryUpdateService
{
    Task UpdateInventoryFromReviewedInvoiceAsync(SupplierInvoice invoice, CancellationToken cancellationToken);

}
