using Pharmacy.Application.Dto.StockHistory;

namespace Pharmacy.Application.Services.Abstraction.StockHistoryService;

public interface IStockHistoryService
{
    Task<bool> LogTransactionAsync(StockHistoryLogDto dto);
    Task<decimal> GetOverallStockLevelAsync(Guid medicineId, decimal quantityChange);

}
