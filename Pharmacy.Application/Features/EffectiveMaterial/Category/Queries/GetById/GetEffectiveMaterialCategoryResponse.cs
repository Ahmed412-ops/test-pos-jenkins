using System.Text.Json.Serialization;
using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.EffectiveMaterial.Category.Queries.GetById;

public class GetEffectiveMaterialCategoryResponse : CommonQueryResponseBase
{
    [JsonPropertyOrder(5)]
    public List<CommonQueryResponseBase> EffectiveMaterials { get; set; } = [];   
}
