using Pharmacy.Application.Dto.Notification;
using Pharmacy.Application.Resources.Static;

namespace Pharmacy.Application.Mapping.PushNotification;

public class PushNotificationProfile : MappingProfileBase
{
    public PushNotificationProfile()
    {
        CreateMap<Domain.Entities.Wallets.Sales.Prescription, PushPrescriptionTransferNotification>()
            .ForMember(dest => dest.PrescriptionId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Message, opt => opt.MapFrom
            (src => Messages.PrescriptionTransferredMessage(src.InvoiceNumber)))
            .ForMember(dest => dest.TransferTime, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src=>src.TransferredByUser!.Full_Name));
    }
}
