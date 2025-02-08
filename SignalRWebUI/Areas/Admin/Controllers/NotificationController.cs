using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.NotificationDtos;
using SignalRWebUI.Areas.Admin.Dtos.NotificationDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using SignalRWebUI.Utilities.FileOperations;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Notification")]
    public class NotificationController : AdminBaseController
    {
        private readonly INotificationServicePS _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationServicePS notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Bildirim";
            ViewBag.EmptyDataInfo = "Bildirim Bilgisi";

            var responseMessage = await _notificationService.GetAllDataAsync(null);

            NotificationViewModel model = new NotificationViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                List<ResultAdminNotificationDto> values = _mapper.Map<List<ResultAdminNotificationDto>>(responseMessage.GetData);
                model.ResultNotifications = values;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateNotification()
        {
            ViewBag.MainTitle = "Bildirim";

            NotificationViewModel model = new NotificationViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateNotification(NotificationViewModel notificationsViewModel)
        {
            notificationsViewModel.CreateNotification.Date = DateTime.Now;
            notificationsViewModel.CreateNotification.Status = false;

            var response = await _notificationService.CreateDataAsync(notificationsViewModel.CreateNotification);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Bildirim Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            notificationsViewModel.HttpResponseMessage = response.StatusMessage;
            notificationsViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(notificationsViewModel);
        }

        [HttpGet]
        [Route("Update/{id}/{status}")]
        public async Task<IActionResult> UpdateNotification(int id, bool status)
        {
            ViewBag.MainTitle = "Bildirim";

            var response = await _notificationService.GetDataWithStatusAsync(id, status);

            var model = new NotificationViewModel();

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                UpdateAdminNotificationDto value = _mapper.Map<UpdateAdminNotificationDto>(response.GetData);

                model.UpdateNotification = value;
            }

            return View(model);
        }

        [HttpGet]
        [Route("UpdateStatus/{id}/{status}")]
        public async Task<IActionResult> UpdateNotificationStatus(int id, bool status)
        {
            var response = await _notificationService.UpdateDataWithStatusAsync(id, status);

            var model = new NotificationViewModel();

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            else
            {
                UpdateAdminNotificationDto value = _mapper.Map<UpdateAdminNotificationDto>(response.GetData);
                model.UpdateNotification = value;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}/{status}")]
        public async Task<IActionResult> UpdateNotification(NotificationViewModel notificationsViewModel)
        {

            var response = await _notificationService.UpdateDataAsync(notificationsViewModel.UpdateNotification);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Bildirim Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            notificationsViewModel.HttpResponseMessage = response.StatusMessage;
            notificationsViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(notificationsViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            await _notificationService.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
