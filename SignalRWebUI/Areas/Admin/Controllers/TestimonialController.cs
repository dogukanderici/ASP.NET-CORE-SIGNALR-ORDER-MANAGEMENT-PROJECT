using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.TestimonialDtos;
using SignalRWebUI.Areas.Admin.Dtos.TestimonialDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using SignalRWebUI.Utilities.FileOperations;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Testimonial")]
    public class TestimonialController : AdminBaseController
    {
        private readonly ITestimonialServicePS _testimonialService;
        private readonly IMapper _mapper;
        private readonly IFileOperation _fileOperation;

        public TestimonialController(ITestimonialServicePS testimonialService, IMapper mapper, IFileOperation fileOperation)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
            _fileOperation = fileOperation;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Yorumlar";
            ViewBag.EmptyDataInfo = "Yorum";

            var responseMessage = await _testimonialService.GetAllDataAsync(null);

            TestimonialViewModel model = new TestimonialViewModel();

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                List<ResultTestimonialDto> values = responseMessage.GetData;
                model.ResultTestimonials = values;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> CreateTestimonial()
        {
            ViewBag.MainTitle = "Yorumlar";

            TestimonialViewModel model = new TestimonialViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTestimonial(TestimonialViewModel testimonialViewModel)
        {

            string imageUrl = await SaveImageFileAsync(testimonialViewModel.CreateTestimonial.Image);
            testimonialViewModel.CreateTestimonial.ImageUrl = imageUrl;

            var response = await _testimonialService.CreateDataAsync(testimonialViewModel.CreateTestimonial);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ResponseMessage = $"Yorum Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

                testimonialViewModel.HttpResponseMessage = response.StatusMessage;
                testimonialViewModel.ApiResponseMessage = response.ApiResponseMessage;
            }

            return View(testimonialViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            ViewBag.MainTitle = "Yorumlar";

            var responseMessage = await _testimonialService.GetDataAsync(id);

            TestimonialViewModel model = new TestimonialViewModel();

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                UpdateAdminTestimonialDto values = _mapper.Map<UpdateAdminTestimonialDto>(responseMessage.GetData);
                model.UpdateTestimonial = values;
            }

            ViewBag.ResponseMessage = $"Yorum Listelenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";

            responseMessage.StatusMessage = responseMessage.StatusMessage;

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}\")")]
        public async Task<IActionResult> UpdateTestimonial(TestimonialViewModel testimonialViewModel)
        {
            if (testimonialViewModel.UpdateTestimonial.Image != null)
            {
                string imageUrl = await SaveImageFileAsync(testimonialViewModel.UpdateTestimonial.Image);
                testimonialViewModel.UpdateTestimonial.ImageUrl = imageUrl;
            }

            var response = await _testimonialService.UpdateDataAsync(testimonialViewModel.UpdateTestimonial);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ResponseMessage = $"Yorum Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

                testimonialViewModel.HttpResponseMessage = response.StatusMessage;
                testimonialViewModel.ApiResponseMessage = response.ApiResponseMessage;
            }

            return View(testimonialViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _testimonialService.DeleteDataAsync(id);

            return RedirectToAction("Index");
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
