using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SignalR.Business.Utilities.Validations.DailyDiscountValidations;
using SignalR.Dtos.Dtos.DailyDiscountDtos;
using SignalRWebUI.Areas.Admin.Dtos.DailyDiscountDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using SignalRWebUI.Utilities.FileOperations;
using System.Collections.Generic;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/DailyDiscount")]
    public class DailyDiscountController : AdminBaseController
    {
        private readonly IDailyDiscountPS _dailyDiscount;
        private readonly IMapper _mapper;
        private readonly IFileOperation _fileOperation;

        public DailyDiscountController(IDailyDiscountPS dailyDiscount, IMapper mapper, IFileOperation fileOperation)
        {
            _dailyDiscount = dailyDiscount;
            _mapper = mapper;
            _fileOperation = fileOperation;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Günlük İndirimler";
            ViewBag.EmptyDataInfo = "Günlük İndirim";

            var responseMessage = await _dailyDiscount.GetAllDataAsync(null);

            var model = new DailyDiscountViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                List<ResultAdminDailyDiscountDto> values = _mapper.Map<List<ResultAdminDailyDiscountDto>>(responseMessage.GetData);
                model.ResultDailyDiscounts = values;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> CreateDailyDiscount()
        {
            ViewBag.MainTitle = "Günlük İndirimler";

            var model = new DailyDiscountViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateDailyDiscount(DailyDiscountViewModel dailydiscountViewModel)
        {
            string imageUrl = await SaveImageFileAsync(dailydiscountViewModel.CreateDailyDiscount.Image);
            dailydiscountViewModel.CreateDailyDiscount.ImageUrl = imageUrl;

            var response = await _dailyDiscount.CreateDataAsync(dailydiscountViewModel.CreateDailyDiscount);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Günlük İndirim Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            dailydiscountViewModel.HttpResponseMessage = response.StatusMessage;
            dailydiscountViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(dailydiscountViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateDailyDiscount(int id)
        {
            ViewBag.MainTitle = "Günlük İndirimler";

            var responseMessage = await _dailyDiscount.GetDataAsync(id);

            var model = new DailyDiscountViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                UpdateAdminDailyDiscountDto values = _mapper.Map<UpdateAdminDailyDiscountDto>(responseMessage.GetData);
                model.UpdateDailyDiscount = values;
            }

            ViewBag.ResponseMessage = $"Günlük İndirim Listelenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}\")")]
        public async Task<IActionResult> UpdateDailyDiscount(DailyDiscountViewModel dailydiscountViewModel)
        {
            if (dailydiscountViewModel.UpdateDailyDiscount.Image != null)
            {
                string imageUrl = await SaveImageFileAsync(dailydiscountViewModel.UpdateDailyDiscount.Image);
                dailydiscountViewModel.UpdateDailyDiscount.ImageUrl = imageUrl;
            }

            var response = await _dailyDiscount.UpdateDataAsync(dailydiscountViewModel.UpdateDailyDiscount);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Günlük İndirim Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            dailydiscountViewModel.HttpResponseMessage = response.StatusMessage;
            dailydiscountViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(dailydiscountViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteDailyDiscount(int id)
        {
            await _dailyDiscount.DeleteDataAsync(id);

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
