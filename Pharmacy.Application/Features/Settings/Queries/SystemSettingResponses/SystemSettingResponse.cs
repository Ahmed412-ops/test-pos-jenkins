using Pharmacy.Domain.Enum;

namespace Pharmacy.Application.Features.Settings.Queries.SystemSettingResponses;

public class SystemSettingResponse
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public object? Value { get; set; }
    public string Type { get; set; } = string.Empty;
}
