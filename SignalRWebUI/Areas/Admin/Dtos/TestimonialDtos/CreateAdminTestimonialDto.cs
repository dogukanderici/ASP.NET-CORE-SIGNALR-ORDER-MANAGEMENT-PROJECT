namespace SignalRWebUI.Areas.Admin.Dtos.TestimonialDtos
{
    public class CreateAdminTestimonialDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public bool Status { get; set; }
    }
}
