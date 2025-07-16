using System.Text.Json.Serialization;
using Pharmacy.Application.Dto.Common.Queries;

namespace Pharmacy.Application.Features.Disease.Disease.Queries.GetById;

public class GetDiseaseResponse : CommonQueryResponseBase
{
    [JsonPropertyOrder(5)]    
    public Guid CategoryId { get; set; }
    [JsonPropertyOrder(6)]    
    public required string CategoryName { get; set; } 
    [JsonPropertyOrder(7)]
    public List<CommonQueryResponseBase> Symptoms { get; set; } = [];
}
