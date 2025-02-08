namespace SignalRWebUI.Areas.Admin.Dtos.MessageDtos
{
    public class UpdateAdminMessageDto
    {
        public Guid MessageID { get; set; }
        public Guid MainMessageID { get; set; }
        public string SenderMail { get; set; }
        public string ReceiverMail { get; set; }
        public string MessageSubject { get; set; }
        public string MessageContent { get; set; }
        public string MessageType { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
