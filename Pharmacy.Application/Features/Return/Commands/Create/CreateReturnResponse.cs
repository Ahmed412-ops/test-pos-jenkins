namespace Pharmacy.Application.Features.Return.Commands.Create;

public class CreateReturnResponse
{
    public decimal TotalRefunded { get; set; }
    public List<ReturnItemResponse> Items { get; set; } = [];
}

public class ReturnItemResponse
{
    public Guid PrescriptionItemId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public string MedicineUnitName { get; set; } = string.Empty;
    public int QuantityReturned { get; set; }
    public decimal AmountRefunded { get; set; }
    public bool IsDamaged { get; set; }
    public string? Reason { get; set; }
}
