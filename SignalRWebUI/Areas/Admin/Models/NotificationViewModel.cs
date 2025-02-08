using SignalRWebUI.Areas.Admin.Dtos.NotificationDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class NotificationViewModel : GenericViewModel
    {
        public List<ResultAdminNotificationDto> ResultNotifications { get; set; }
        public CreateAdminNotificationDto CreateNotification { get; set; }
        public UpdateAdminNotificationDto UpdateNotification { get; set; }
    }
}