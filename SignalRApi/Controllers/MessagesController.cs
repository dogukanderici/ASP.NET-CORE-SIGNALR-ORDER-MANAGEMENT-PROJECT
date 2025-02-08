using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.MessageDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMessageDto> _createValidator;
        private readonly IValidator<UpdateMessageDto> _updateValidator;

        public MessagesController(IMessageService messageService, IMapper mapper, IValidator<CreateMessageDto> createValidator, IValidator<UpdateMessageDto> updateValidator)
        {
            _messageService = messageService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult GetMessageList()
        {
            IDataResult<List<Message>> values = _messageService.TGetAllData(null, null);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(_mapper.Map<List<Message>>(values.Data));
        }

        [HttpGet("GetMessageById")]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult GetMessageById(Guid id)
        {
            IDataResult<Message> value = _messageService.TGetData(x => x.MessageID == id, null);

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)value.SuccessCode, value);
            }

            return Ok(_mapper.Map<Message>(value.Data));
        }

        [HttpGet("GetMessageByMainId")]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult GetMessageByMainId(Guid id)
        {
            IDataResult<List<Message>> value = _messageService.TGetAllData(x => x.MainMessageID == id, null);

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)value.SuccessCode, value);
            }

            return Ok(_mapper.Map<List<Message>>(value.Data));
        }

        [HttpGet("GetInboxMessage")]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult GetInboxMessage(string receiverMail)
        {
            IDataResult<List<Message>> values = _messageService.TGetAllData(x => x.ReceiverMail == receiverMail, null);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(_mapper.Map<List<Message>>(values.Data));
        }

        [HttpGet("GetOutboxMessage")]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult GetOutboxMessage(string senderMail)
        {
            IDataResult<List<Message>> values = _messageService.TGetAllData(x => x.SenderMail == senderMail, null);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(_mapper.Map<List<Message>>(values.Data));
        }

        [HttpPost]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            var validatorResult = _createValidator.Validate(createMessageDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Message valueFromDto = _mapper.Map<Message>(createMessageDto);
            IResult response = _messageService.TAddData(valueFromDto);

            if (response.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.SuccessCode, response);
            }

            return Ok(new { success = true, message = "Mesaj Verisi Başarıyla Kaydedildi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            var validatorResult = _updateValidator.Validate(updateMessageDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Message valueFromDto = _mapper.Map<Message>(updateMessageDto);
            IResult response = _messageService.TUpdateData(valueFromDto);

            if (response.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.SuccessCode, response);
            }

            return Ok(new { success = true, message = "Mesaj Verisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteMessage(int id)
        {
            IDataResult<bool> response = _messageService.TDeleteData(id);

            if (response.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.SuccessCode, response);
            }

            return Ok(new { success = true, message = "Mesaj Verisi Başarıyla Silindi." });
        }
    }
}
