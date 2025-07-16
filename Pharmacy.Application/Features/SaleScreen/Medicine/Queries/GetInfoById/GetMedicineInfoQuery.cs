using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.SaleScreen.Medicine.Queries.GetInfoById;

public class GetMedicineInfoQuery : IRequest<Result<GetMedicineInfoResponse>>
{
    public Guid Id { get; set; }
    public decimal? Weight { get; set; } = null;
}
