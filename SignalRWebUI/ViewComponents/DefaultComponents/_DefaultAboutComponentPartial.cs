using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.AboutDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly IDefaultAboutServicePS _aboutServicePS;
        private readonly IMapper _mapper;

        public _DefaultAboutComponentPartial(IDefaultAboutServicePS aboutServicePS, IMapper mapper)
        {
            _aboutServicePS = aboutServicePS;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _aboutServicePS.GetAllDataAsync(null);

            DefaultAboutViewModel model = new DefaultAboutViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = "Bu Sayfayı Görüntülemek İçin Yetkiniz Bulunmamaktadır!";
            }

            model.ResultAbouts = response.GetData;

            return View(model);
        }
    }
}
