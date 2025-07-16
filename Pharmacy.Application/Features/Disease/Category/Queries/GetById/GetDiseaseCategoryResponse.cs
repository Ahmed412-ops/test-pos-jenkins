using System.Text.Json.Serialization;
using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Disease.Category.Queries.GetById;

public class GetDiseaseCategoryResponse : CommonQueryResponseBase
{
    [JsonPropertyOrder(5)]
    public List<CommonQueryResponseBase> Diseases { get; set; } = [];
}
