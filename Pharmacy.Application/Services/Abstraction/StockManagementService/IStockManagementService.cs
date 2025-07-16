namespace Pharmacy.Application.Services.Abstraction.StockManagementService;

public interface IStockManagementService
{
    Task ReserveAsync(IEnumerable<(Guid stockId, int quantity)> items, Guid shiftWalletId);
}
