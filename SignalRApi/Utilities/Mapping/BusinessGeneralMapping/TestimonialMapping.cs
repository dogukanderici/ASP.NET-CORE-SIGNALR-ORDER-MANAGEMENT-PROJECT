using AutoMapper;
using SignalR.Dtos.Dtos.TestimonialDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class TestimonialMapping : Profile
    {
        public TestimonialMapping()
        {
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
        }
    }
}
