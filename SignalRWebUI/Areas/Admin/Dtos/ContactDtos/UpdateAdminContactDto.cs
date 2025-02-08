namespace SignalRWebUI.Areas.Admin.Dtos.ContactDtos
{
    public class UpdateAdminContactDto
    {
        public int ContactID { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Description { get; set; }
    }
}
