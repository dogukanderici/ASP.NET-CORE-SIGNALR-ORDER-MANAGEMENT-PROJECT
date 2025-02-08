namespace SignalRWebUI.Areas.Admin.Dtos.SocialMediaDtos
{
    public class UpdateAdminSocialMediaDto
    {
        public int SocialMediaID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool Status { get; set; }
    }
}
