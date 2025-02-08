using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.NotificationDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Authorize(Policy = "AdminPermissionPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : BaseController
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateNotificationDto> _createValidator;
        private readonly IValidator<UpdateNotificationDto> _updateValidator;

        public NotificationsController(INotificationService notificationService, IMapper mapper, IValidator<CreateNotificationDto> createValidator, IValidator<UpdateNotificationDto> updateValidator)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public IActionResult NotificationList()
        {
            IDataResult<List<Notification>> values = _notificationService.TGetAllData(null, null);
            List<ResultNotificationDto> datas = new List<ResultNotificationDto>();

            if (values.SuccessCode == HttpStatusCode.OK)
            {
                datas = _mapper.Map<List<ResultNotificationDto>>(values.Data);
            }

            return Ok(datas);
        }

        [HttpGet("GetNotificationsById")]
        public IActionResult GetNotificationsById(int id, bool status)
        {
            IDataResult<Notification> values = _notificationService.TGetData(n => n.Status == status && n.NotificationID == id, null);
            ResultNotificationDto data = new ResultNotificationDto();

            if (values.SuccessCode == HttpStatusCode.OK)
            {
                data = _mapper.Map<ResultNotificationDto>(values.Data);
            }

            return Ok(data);
        }

        [HttpGet("GetUnreadNotificationCount")]
        public IActionResult GetUnreadNotificationCount()
        {
            IDataResult<int> value = _notificationService.GetUnreadNotificationCount();

            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var validatorResult = _createValidator.Validate(createNotificationDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Notification valueFromDto = _mapper.Map<Notification>(createNotificationDto);
            IResult response = _notificationService.TAddData(valueFromDto);

            if (response.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.SuccessCode, response);
            }

            return Ok(new { success = true, message = "Bildirim Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var validatorResult = _updateValidator.Validate(updateNotificationDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Notification valueFromDto = _mapper.Map<Notification>(updateNotificationDto);
            IResult response = _notificationService.TUpdateData(valueFromDto);

            if (response.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.SuccessCode, response);
            }

            return Ok(new { success = true, message = "Bildirim Verisi Başarıyla Güncellendi." });
        }

        [HttpGet("ChangeNotificationStatus")]
        public IActionResult ChangeNotificationStatus(int id, bool status)
        {
            IResult response = _notificationService.TUpdateNotificationStatus(id, status);

            if (response.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.SuccessCode, response);
            }

            return Ok(new { success = true, message = "Bildirim Durumu " + (status ? "Okundu" : "Okunmadı") + " Olarak Başarıyla Güncellendi." });
        }

        [HttpDelete]
        public IActionResult DeleteNotification(int id)
        {
            IDataResult<bool> response = _notificationService.TDeleteData(id);

            if (!response.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Bildirim Verisi Başarıyla Güncellendi." });
        }
    }
}
