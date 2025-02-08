using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.Dtos.Dtos.TestimonialDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultTestimonailComponentPartial : ViewComponent
    {
        private readonly ITestimonialServicePS _testimonailService;
        private readonly IMapper _mapper;

        public _DefaultTestimonailComponentPartial(ITestimonialServicePS testimonailService, IMapper mapper)
        {
            _testimonailService = testimonailService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _testimonailService.GetAllDataAsync(null);

            DefaultTestimonialViewModel model = new DefaultTestimonialViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = "Bu Sayfayı Görüntülemek İçin Yetkiniz Bulunmamaktadır!";
            }

            List<ResultTestimonialDto> values = _mapper.Map<List<ResultTestimonialDto>>(response.GetData);
            model.ResultTestimonials = values;

            return View(model);
        }
    }
}
