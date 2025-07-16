using Pharmacy.Application.Dto.StockHistory;
using Pharmacy.Application.Features.Stock.History.Queries.GetAll;
using Pharmacy.Domain.Entities.Stock;

namespace Pharmacy.Application.Mapping.Stock;

public class StockHistoryProfile : MappingProfileBase
{
    public StockHistoryProfile()
    {
        CreateMap<StockHistoryLogDto, StockHistory>();
        CreateMap<StockHistory, GetStockHistoryResponse>()
            .ForMember(d => d.MedicineName, s => s.MapFrom(a => a.Medicine.Name))
            .ForMember(d => d.TransactionType, s => s.MapFrom(a => a.TransactionType.ToString()))
            .ForMember(d => d.PerformedBy, s => s.MapFrom(a => a.PerformedBy.UserName));
            
    }
}
