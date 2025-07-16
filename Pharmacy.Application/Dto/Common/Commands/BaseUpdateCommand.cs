using System.Text.Json.Serialization;

namespace Pharmacy.Application.Dto.Common.Commands;

public interface IBaseUpdateCommand : IBaseCommand
{
    [JsonPropertyOrder(1)]
    Guid Id { get; set; }
}
