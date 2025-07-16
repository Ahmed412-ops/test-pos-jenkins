namespace Pharmacy.Application.Dto.Notification;

public class PushPrescriptionTransferNotification
{
    public Guid PrescriptionId { get; set; }
    public int InvoiceNumber { get; set; }
    public string? SenderName { get; set; } 
    public string? Message { get; set; } 
    public DateTime TransferTime { get; set; } = DateTime.UtcNow;
}
