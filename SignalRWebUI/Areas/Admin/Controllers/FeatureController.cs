using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SignalRWebUI.Areas.Admin.Dtos.AboutDtos;
using SignalRWebUI.Areas.Admin.Dtos.FeatureDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Feature")]
    public class FeatureController : AdminBaseController
    {
        private readonly IDefaultFeatureServicePS _featureService;
        private readonly ApiSettings _apiSettings;
        private readonly IMapper _mapper;

        public FeatureController(IDefaultFeatureServicePS featureService, IOptions<ApiSettings> apiSettings, IMapper mapper)
        {
            _featureService = featureService;
            _apiSettings = apiSettings.Value;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Öne Çıkanlar";
            ViewBag.EmptyDataInfo = "Öne Çıkan";

            var response = await _featureService.GetAllDataAsync(null);

            var model = new FeatureViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = response.StatusMessage;
            }
            else
            {
                model.ResultFeatures = _mapper.Map<List<ResultAdminFeatureDto>>(response.GetData); ;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult AddFeature()
        {
            var model = new FeatureViewModel();

            ViewBag.MainTitle = "Öne Çıkanlar";

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddFeature(FeatureViewModel featureViewModel)
        {
            var response = await _featureService.CreateDataAsync(featureViewModel.CreateFeature);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            featureViewModel.HttpResponseMessage = response.StatusMessage;
            featureViewModel.ApiResponseMessage = response.ApiResponseMessage;

            ViewBag.ResponseMessage = $"Öne Çıkan Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode} - {response.StatusMessage}";

            return View(featureViewModel);
        }



        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateFeature(int id)
        {
            ViewBag.MainTitle = "Öne Çıkanlar";

            var response = await _featureService.GetDataAsync(id);

            var model = new FeatureViewModel();

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                UpdateAdminFeatureDto values = _mapper.Map<UpdateAdminFeatureDto>(response.GetData);

                model.UpdateFeature = values;
            }
            else
            {
                model.HttpResponseMessage = response.StatusMessage;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateFeature(FeatureViewModel featureViewModel)
        {

            var response = await _featureService.UpdateDataAsync(featureViewModel.UpdateFeature);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                featureViewModel.HttpResponseMessage = response.StatusMessage;
                featureViewModel.ApiResponseMessage = response.ApiResponseMessage;
            }

            ViewBag.ResponseMessage = $"Öne Çıkan Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode} - {response.StatusMessage}";

            return View(featureViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            await _featureService.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
