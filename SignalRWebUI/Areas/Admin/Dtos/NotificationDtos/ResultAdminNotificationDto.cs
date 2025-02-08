namespace SignalRWebUI.Areas.Admin.Dtos.NotificationDtos
{
    public class ResultAdminNotificationDto
    {
        public int NotificationID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
