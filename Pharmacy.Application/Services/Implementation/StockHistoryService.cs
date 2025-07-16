using AutoMapper;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Services.Abstraction.StockHistoryService;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Application.Services.Implementation;

public class StockHistoryService(IUnitOfWork unitOfWork, IMapper mapper) : IStockHistoryService
{
    private readonly IGenericRepository<StockHistory> _stockHistoryRepository =
        unitOfWork.GetRepository<StockHistory>();

    private readonly IGenericRepository<MedicationStock> _medicationStockRepository =
        unitOfWork.GetRepository<MedicationStock>();

    public async Task<bool> LogTransactionAsync(StockHistoryLogDto dto)
    {
        var stockHistory = mapper.Map<StockHistory>(dto);
        await _stockHistoryRepository.AddAsync(stockHistory);
        return true;
    }

    public async Task<decimal> GetOverallStockLevelAsync(Guid medicineId, decimal quantityChange)
    {
        var query = await _medicationStockRepository.GetAllQueryableAsync(x =>
            x.MedicineId == medicineId && !x.Is_Deleted
        );

        var overallStockLevel = query.Sum(x => x.Quantity);
        overallStockLevel += quantityChange;
        return overallStockLevel;
    }
}
