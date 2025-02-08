using SignalR.Dtos.Dtos.TestimonialDtos;
using SignalRWebUI.Areas.Admin.Dtos.TestimonialDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class TestimonialViewModel : GenericViewModel
    {
        public List<ResultTestimonialDto> ResultTestimonials { get; set; }
        public CreateAdminTestimonialDto CreateTestimonial { get; set; }
        public UpdateAdminTestimonialDto UpdateTestimonial { get; set; }
    }
}
