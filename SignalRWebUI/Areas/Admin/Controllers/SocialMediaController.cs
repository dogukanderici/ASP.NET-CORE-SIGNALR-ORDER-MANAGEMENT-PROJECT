using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.SocialMediaDtos;
using SignalRWebUI.Areas.Admin.Dtos.SocialMediaDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using SignalRWebUI.Utilities.FileOperations;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SocialMedia")]
    public class SocialMediaController : AdminBaseController
    {

        private readonly ISocialMediaServicePS _socialMediaService;
        private readonly IMapper _mapper;

        public SocialMediaController(ISocialMediaServicePS socialMediaService, IMapper mapper)
        {
            _socialMediaService = socialMediaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Sosyal Medya Hesapları";
            ViewBag.EmptyDataInfo = "Sosyal Medya";

            var responseMessage = await _socialMediaService.GetAllDataAsync(null);

            SocialMediaViewModel model = new SocialMediaViewModel();

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                List<ResultSocialMediaDto> values = responseMessage.GetData;
                model.ResultSocialMedias = values;
            }

            model.HttpResponseMessage = responseMessage.StatusMessage;

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> CreateSocialMedia()
        {
            ViewBag.MainTitle = "Sosyal Medya Hesapları";

            SocialMediaViewModel model = new SocialMediaViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateSocialMedia(SocialMediaViewModel socialmediaViewModel)
        {

            var response = await _socialMediaService.CreateDataAsync(socialmediaViewModel.CreateSocialMedia);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ResponseMessage = $"Sosyal Medya Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

                socialmediaViewModel.HttpResponseMessage = response.StatusMessage;
                socialmediaViewModel.ApiResponseMessage = response.ApiResponseMessage;
            }

            return View(socialmediaViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            ViewBag.MainTitle = "Sosyal Medya Hesapları";

            var responseMessage = await _socialMediaService.GetDataAsync(id);

            var model = new SocialMediaViewModel();

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                UpdateAdminSocialMediaDto values = _mapper.Map<UpdateAdminSocialMediaDto>(responseMessage.GetData);
                model.UpdateSocialMedia = values;
            }

            ViewBag.ResponseMessage = $"Sosyal Medya Listelenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";

            responseMessage.StatusMessage = responseMessage.StatusMessage;

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}\")")]
        public async Task<IActionResult> UpdateSocialMedia(SocialMediaViewModel socialmediaViewModel)
        {
            var response = await _socialMediaService.UpdateDataAsync(socialmediaViewModel.UpdateSocialMedia);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ResponseMessage = $"Sosyal Medya Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

                socialmediaViewModel.HttpResponseMessage = response.StatusMessage;
                socialmediaViewModel.ApiResponseMessage = response.ApiResponseMessage;
            }

            return View(socialmediaViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {

            await _socialMediaService.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
