using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Application.Services.Abstraction.StockManagementService;
using Pharmacy.Domain.Entities.Stock;
using Pharmacy.Domain.Enum;

public class StockManagementService(
    IUnitOfWork unitOfWork,
    IStockHistoryService historyService,
    ICurrentUser currentUser
) : IStockManagementService
{
    private readonly IGenericRepository<MedicationStock> _stockRepo =
        unitOfWork.GetRepository<MedicationStock>();
    private readonly IStockHistoryService _historyService = historyService;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task ReserveAsync(
        IEnumerable<(Guid stockId, int quantity)> items,
        Guid shiftWalletId
    )
    {
        var ids = items.Select(x => x.stockId).Distinct().ToList();
        var stocks = await _stockRepo.GetAllAsync(
            s => ids.Contains(s.Id),
            Include: q => q.Include(s => s.Medicine)
        );

        var stockDict = stocks.ToDictionary(s => s.Id);
        foreach (var (stockId, qty) in items)
        {
            if (!stockDict.TryGetValue(stockId, out var st))
                throw new InvalidOperationException(Messages.MedicationStockNotFound);

            if (st.Quantity < qty)
                throw new InvalidOperationException(Messages.QuantityExceedsAvailableStock);
        }

        Guid? prerformedById = _currentUser.GetUserId();
        foreach (var (stockId, qty) in items)
        {
            var st = stockDict[stockId];
            st.Quantity -= qty;

            var dto = new StockHistoryLogDto
            {
                MedicineId = st.MedicineId,
                TransactionType = StockTransactionType.Sold,
                QuantityChange = -qty,
                UpdatedStockLevel = await _historyService.GetOverallStockLevelAsync(
                    st.MedicineId,
                    -qty
                ),
                PerformedById = prerformedById ?? Guid.Empty,
                TransactionDate = DateTime.UtcNow,
                ReasonForChange = $"Sold in shift {shiftWalletId}",
            };

            await _historyService.LogTransactionAsync(dto);
        }
    }
}
