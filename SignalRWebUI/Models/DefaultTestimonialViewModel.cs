using SignalR.Dtos.Dtos.AboutDtos;
using SignalR.Dtos.Dtos.TestimonialDtos;

namespace SignalRWebUI.Models
{
    public class DefaultTestimonialViewModel
    {
        public List<ResultTestimonialDto> ResultTestimonials { get; set; }

        public string HttpResponseMessage { get; set; }
    }
}
