namespace Pharmacy.Application.Dto.Common.Commands;

public interface IBaseCommand
{
    string Name { get; set; }
    string? Description { get; set; }
    string? Notes { get; set; }
}
