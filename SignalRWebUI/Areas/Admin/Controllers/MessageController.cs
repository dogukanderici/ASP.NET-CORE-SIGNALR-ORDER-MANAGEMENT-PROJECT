using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.MessageDtos;
using SignalRWebUI.Areas.Admin.Dtos.MessageDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Message")]
    public class MessageController : AdminBaseController
    {
        private readonly IMessageServicePS _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageServicePS messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllMessages")]
        public async Task<IActionResult> GetAllMessages()
        {
            ViewBag.MainTitle = "Mesajlar";
            ViewBag.EmptyDataInfo = "Mesaj Bilgisi";

            string email = User?.Claims?.Where(c => c.Type == "email").Select(c => c.Value).FirstOrDefault();

            var responseInbox = await _messageService.GetAllInboxMessageAsync(email);

            MessageViewModel model = new MessageViewModel();

            if (responseInbox.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseInbox.StatusMessage;
            }
            else
            {
                List<ResultAdminMessageDto> inboxValues = _mapper.Map<List<ResultAdminMessageDto>>(responseInbox.GetData);
                model.ResultInboxMessages = inboxValues;
            }

            var responseOutbox = await _messageService.GetAllOutboxMessageAsync(email);

            if (responseOutbox.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseOutbox.StatusMessage;
            }
            else
            {
                List<ResultAdminMessageDto> outboxValues = _mapper.Map<List<ResultAdminMessageDto>>(responseOutbox.GetData);
                model.ResultOutboxMessages = outboxValues;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> DetailMessage(Guid id)
        {
            ViewBag.MainTitle = "Mesaj Detayı";

            MessageViewModel model = new MessageViewModel();

            var response = await _messageService.GetDataByGuid(id);

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = response.StatusMessage;
            }
            else
            {
                ResultAdminMessageDto value = _mapper.Map<ResultAdminMessageDto>(response.GetData);
                model.MessageData = value;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateMessage()
        {
            ViewBag.MainTitle = "Mesajlar";

            MessageViewModel model = new MessageViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateMessage(MessageViewModel messageViewModel)
        {
            if ((messageViewModel.CreateMessage.MainMessageID == null) || (messageViewModel.CreateMessage.MainMessageID == Guid.Empty))
            {
                messageViewModel.CreateMessage.MessageID = Guid.NewGuid();
                messageViewModel.CreateMessage.MainMessageID = messageViewModel.CreateMessage.MessageID;
            }

            string email = User?.Claims?.Where(c => c.Type == "email").Select(c => c.Value).FirstOrDefault();

            messageViewModel.CreateMessage.MessageType = "Message";
            messageViewModel.CreateMessage.SendDate = DateTime.Now;
            messageViewModel.CreateMessage.SenderMail = email;

            var response = await _messageService.CreateDataAsync(messageViewModel.CreateMessage);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllMessages");
            }

            ViewBag.ResponseMessage = $"Yeni Mesaj Gönderilirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            messageViewModel.HttpResponseMessage = response.StatusMessage;
            messageViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(messageViewModel);
        }

        [HttpGet]
        [Route("Update/{id}/{customer}")]
        public async Task<IActionResult> UpdateMessage(Guid id, string customer)
        {
            ViewBag.MainTitle = "Mesajlar";

            string email = User?.Claims?.Where(c => c.Type == "email").Select(c => c.Value).FirstOrDefault();

            var response = await _messageService.GetAllMainMessageAsync(id);

            MessageViewModel model = new MessageViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = response.StatusMessage;
            }
            else
            {
                List<ResultAdminMessageDto> value = _mapper.Map<List<ResultAdminMessageDto>>(response.GetData);
                model.ResultInboxMessages = value;

                model.UpdateMessage = new UpdateAdminMessageDto();
                model.UpdateMessage.MessageSubject = value[0].MessageSubject;
                model.UpdateMessage.MainMessageID = value[0].MainMessageID;

                model.UpdateMessage.ReceiverMail = value[0].ReceiverMail;
                model.UpdateMessage.SenderMail = customer;

                ViewBag.CustomerMail = customer;
                ViewBag.UserMail = email;

            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}/{customer}")]
        public async Task<IActionResult> UpdateMessage(MessageViewModel messageViewModel)
        {
            var id = Guid.Parse(HttpContext.Request.RouteValues["id"].ToString());
            var customer = HttpContext.Request.RouteValues["customer"].ToString();

            string email = User?.Claims?.Where(c => c.Type == "email").Select(c => c.Value).FirstOrDefault();

            messageViewModel.CreateMessage = new CreateAdminMessageDto();
            messageViewModel.CreateMessage.MessageID = Guid.NewGuid();
            messageViewModel.CreateMessage.MainMessageID = messageViewModel.UpdateMessage.MainMessageID;
            messageViewModel.CreateMessage.ReceiverMail = messageViewModel.UpdateMessage.SenderMail;
            messageViewModel.CreateMessage.SenderMail = email;
            messageViewModel.CreateMessage.MessageSubject = messageViewModel.UpdateMessage.MessageSubject;
            messageViewModel.CreateMessage.MessageType = "Message";
            messageViewModel.CreateMessage.MessageContent = messageViewModel.UpdateMessage.MessageContent;
            messageViewModel.CreateMessage.SendDate = DateTime.Now;
            messageViewModel.CreateMessage.IsRead = false;

            var response = await _messageService.CreateDataAsync(messageViewModel.CreateMessage);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("UpdateMessage", new { id = id, customer = customer });
            }

            MessageViewModel model = new MessageViewModel();

            model.HttpResponseMessage = response.StatusMessage;
            model.ApiResponseMessage = response.ApiResponseMessage;

            return View(model);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _messageService.DeleteDataAsync(id);

            return View();
        }
    }
}