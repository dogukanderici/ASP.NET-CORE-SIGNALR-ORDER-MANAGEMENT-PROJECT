using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SignalRWebUI.Areas.Admin.Dtos.AboutDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using SignalRWebUI.Utilities.FileOperations;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : AdminBaseController
    {
        private readonly ApiSettings _apiSettings;
        private IFileOperation _fileOperation;
        private readonly IMapper _mapper;

        private readonly IDefaultAboutServicePS _aboutService;

        public AboutController(IOptions<ApiSettings> apiSettings, IFileOperation fileOperation, IDefaultAboutServicePS aboutService, IMapper mapper)
        {
            _apiSettings = apiSettings.Value;
            _fileOperation = fileOperation;
            _aboutService = aboutService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Hakkımızda";
            ViewBag.EmptyDataInfo = "Hakkımızda Bilgisi";

            var model = new AboutViewModel();

            var values = await _aboutService.GetAllDataAsync(null);

            if (values.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = values.StatusMessage;
            }
            else
            {
                model.ResultAbouts = _mapper.Map<List<ResultAdminAboutDto>>(values.GetData);
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateAbout()
        {
            var model = new AboutViewModel();

            ViewBag.MainTitle = "Hakkımızda";

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAbout(AboutViewModel aboutViewModel)
        {
            var response = await _aboutService.CreateDataAsync(aboutViewModel.CreateAbout);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            aboutViewModel.HttpResponseMessage = response.StatusMessage;
            aboutViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(aboutViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            ViewBag.MainTitle = "Hakkımızda";

            var response = await _aboutService.GetDataAsync(id);

            var model = new AboutViewModel();

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                UpdateAdminAboutDto value = _mapper.Map<UpdateAdminAboutDto>(response.GetData);

                model.UpdateAbout = value;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAbout(AboutViewModel aboutViewModel)
        {
            if (aboutViewModel.UpdateAbout.Image != null)
            {
                string imageUrl = await SaveImageFileAsync(aboutViewModel.UpdateAbout.Image);
                aboutViewModel.UpdateAbout.ImageUrl = imageUrl;
            }

            var response = await _aboutService.UpdateDataAsync(aboutViewModel.UpdateAbout);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            aboutViewModel.HttpResponseMessage = response.StatusMessage;
            aboutViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(aboutViewModel);
        }

        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {
            FileSettings fileSettings = new FileSettings
            {
                FolderPath = "\\wwwroot\\asset",
                FolderName = "defaultimage",
                UploadFile = imageFile
            };

            return await _fileOperation.SaveFileAsync(fileSettings);
        }
    }
}
