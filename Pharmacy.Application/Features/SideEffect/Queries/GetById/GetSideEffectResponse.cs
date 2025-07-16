using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.SideEffect.Queries.GetById;

public class GetSideEffectResponse : CommonQueryResponseBase
{
    public List<string> EffectiveMaterials { get; set; } = [];
}
