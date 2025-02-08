using SignalR.Dtos.Dtos.TestimonialDtos;
using SignalRWebUI.Areas.Admin.Dtos.TestimonialDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface ITestimonialServicePS : IDefaultGenericService<ResultTestimonialDto, List<ResultTestimonialDto>, CreateAdminTestimonialDto, UpdateAdminTestimonialDto>
    {
    }
}
