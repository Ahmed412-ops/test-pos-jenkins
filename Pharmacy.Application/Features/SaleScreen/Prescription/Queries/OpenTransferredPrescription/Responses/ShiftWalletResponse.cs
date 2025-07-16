namespace Pharmacy.Application.Features.SaleScreen.Prescription.Queries.OpenTransferredPrescription.Responses;

public class ShiftWalletResponse
{
    public Guid ShiftId { get; set; }
    public Guid WalletId { get; set; }
    public string WalletName { get; set; } = null!;
    public decimal OpeningBalance { get; set; }
}