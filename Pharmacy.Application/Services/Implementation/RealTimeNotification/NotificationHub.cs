using Microsoft.AspNetCore.SignalR;
using Pharmacy.Application.Services.Abstraction.NotificationServices;

namespace Pharmacy.Application.Services.Implementation.RealTimeNotification;
    public class NotificationHub : Hub<IRealTimeNotification>
    {
        
    }
