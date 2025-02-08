namespace SignalRWebUI.Areas.Admin.Dtos.AboutDtos
{
    public class UpdateAdminAboutDto
    {
        public int AboutID { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
