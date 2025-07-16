using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Wallet.Queries.GetAll;

public class GetWalletsResponse : CommonQueryResponseBase
{
    public required string Type { get; set; }
    public bool IsDefaultForCashPayments { get; set; } = false;
}
