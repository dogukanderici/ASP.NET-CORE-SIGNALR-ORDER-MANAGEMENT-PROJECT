namespace SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos
{
    public class CreateAdminHelpDeskDto
    {
        public Guid HelpDeskID { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
        public Guid ReplyID { get; set; }
    }
}
