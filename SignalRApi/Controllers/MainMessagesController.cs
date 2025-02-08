using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.MainMessageDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainMessagesController : BaseController
    {
        private readonly IMainMessageService _mainMessageService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMainMessageDto> _createValidator;
        private readonly IValidator<UpdateMainMessageDto> _updateValidator;

        public MainMessagesController(IMainMessageService mainMessageService, IMapper mapper, IValidator<CreateMainMessageDto> createValiadtor, IValidator<UpdateMainMessageDto> updateValiadtor)
        {
            _mainMessageService = mainMessageService;
            _mapper = mapper;
            _createValidator = createValiadtor;
            _updateValidator = updateValiadtor;
        }

        [HttpGet]
        public IActionResult GetMainMessages()
        {
            IDataResult<List<MainMessage>> values = _mainMessageService.TGetAllData(null, null);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(_mapper.Map<List<ResultMainMessageDto>>(values.Data));
        }

        [HttpGet("GetMainMessageById")]
        //[Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult GetMainMessageById(int id)
        {
            IDataResult<MainMessage> values = _mainMessageService.TGetData(mm => mm.MainMessageID == id, mm => mm.Messages);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(_mapper.Map<ResultMainMessageDto>(values.Data));
        }

        [HttpPost]
        //[Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult CreateMainMessage(CreateMainMessageDto createMainMessageDto)
        {
            var validatorResult = _createValidator.Validate(createMainMessageDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            MainMessage valueFromDto = _mapper.Map<MainMessage>(createMainMessageDto);
            IResult values = _mainMessageService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Ana Mesaj Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        //[Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateMainMessage(UpdateMainMessageDto updateMainMessageDto)
        {
            var validatorResult = _updateValidator.Validate(updateMainMessageDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            MainMessage valueFromDto = _mapper.Map<MainMessage>(updateMainMessageDto);
            IResult values = _mainMessageService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Ana Mesaj Verisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        //[Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteMainMessage(int id)
        {
            IDataResult<bool> values = _mainMessageService.TDeleteData(id);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Ana Mesaj Verisi Başarıyla Silindi." });
        }
    }
}
