namespace SignalRWebUI.Areas.Admin.Dtos.AboutDtos
{
    public class CreateAdminAboutDto
    {
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
