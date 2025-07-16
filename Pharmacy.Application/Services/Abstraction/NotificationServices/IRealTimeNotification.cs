using Pharmacy.Application.Dto.Notification;

namespace Pharmacy.Application.Services.Abstraction.NotificationServices;
    public interface IRealTimeNotification
    {
        Task SendNotification(PushPrescriptionTransferNotification notificationDto);
    }
