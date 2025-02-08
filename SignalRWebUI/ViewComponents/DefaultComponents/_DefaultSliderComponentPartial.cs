using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.AboutDtos;
using SignalR.Dtos.Dtos.FeatureDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultSliderComponentPartial : ViewComponent
    {
        private readonly IDefaultFeatureServicePS _featureServicePS;
        private readonly IMapper _mapper;

        public _DefaultSliderComponentPartial(IDefaultFeatureServicePS featureServicePS, IMapper mapper)
        {
            _featureServicePS = featureServicePS;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _featureServicePS.GetAllDataAsync(null);

            DefaultFeatureViewModel model = new DefaultFeatureViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = "Bu Sayfayı Görüntülemek İçin Yetkiniz Bulunmamaktadır!";
            }

            List<ResultFeatureDto> values = _mapper.Map<List<ResultFeatureDto>>(response.GetData);
            model.ResultFeatures = values;

            return View(model);
        }
    }
}
