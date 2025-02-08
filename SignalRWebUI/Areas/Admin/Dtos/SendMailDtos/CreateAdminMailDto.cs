namespace SignalRWebUI.Areas.Admin.Dtos.SendMailDtos
{
    public class CreateAdminMailDto
    {
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
