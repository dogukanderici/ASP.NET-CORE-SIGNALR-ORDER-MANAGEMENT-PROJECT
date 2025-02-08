using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.HelpDeskDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Authorize(Policy = "ReadPermissionPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class HelpDesksController : BaseController
    {
        private readonly IHelpDeskService _helpDeskService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateHelpDeskDto> _createValidator;
        private readonly IValidator<UpdateHelpDeskDto> _updateValidator;

        public HelpDesksController(IHelpDeskService helpDeskService, IMapper mapper, IValidator<CreateHelpDeskDto> createValidator, IValidator<UpdateHelpDeskDto> updateValidator)
        {
            _helpDeskService = helpDeskService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public IActionResult HelpDeskList()
        {
            IDataResult<List<HelpDesk>> values = _helpDeskService.TGetAllData(null);
            List<ResultHelpDeskDto> valueToDto = _mapper.Map<List<ResultHelpDeskDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetHelpDeskById")]
        public IActionResult HelpDeskData(Guid id)
        {
            IDataResult<HelpDesk> value = _helpDeskService.TGetData(hd => hd.HelpDeskID == id);
            ResultHelpDeskDto valueToDto = _mapper.Map<ResultHelpDeskDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetHelpDeskForInboxById")]
        public IActionResult HelpDeskForInboxById()
        {
            // Müşteri tarafından ilk gönderilen mesajı filtereler ve veriyi getirir.
            IDataResult<List<HelpDesk>> values = _helpDeskService.TGetAllData(hd => hd.Mail != "admin@testmail.com" && hd.ReplyID == hd.HelpDeskID);
            List<ResultHelpDeskDto> valueToDto = _mapper.Map<List<ResultHelpDeskDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetHelpDeskForOutboxById")]
        public IActionResult HelpDeskForOutboxById(Guid id)
        {
            // Müşteri tarafından ilk gönderilen mesajı filtereler ve veriyi getirir.
            IDataResult<List<HelpDesk>> values = _helpDeskService.TGetAllData(hd => hd.Mail == "admin@testmail.com" && hd.ReplyID == id);
            List<ResultHelpDeskDto> valueToDto = _mapper.Map<List<ResultHelpDeskDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetHelpDeskForUserOutboxById")]
        public IActionResult HelpDeskForUserOutboxById(string userMail)
        {
            // Müşteri tarafından ilk gönderilen mesajı filtereler ve veriyi getirir.
            IDataResult<List<HelpDesk>> values = _helpDeskService.TGetAllData(hd => hd.Mail == userMail && hd.ReplyID == hd.HelpDeskID);
            List<ResultHelpDeskDto> valueToDto = _mapper.Map<List<ResultHelpDeskDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetHelpDeskForUserInboxById")]
        public IActionResult HelpDeskForUserInboxById(string userMail, Guid replyId)
        {
            // Müşteri tarafından ilk gönderilen mesajı filtereler ve veriyi getirir.
            IDataResult<List<HelpDesk>> values = _helpDeskService.TGetAllData(hd => hd.Mail == userMail && hd.ReplyID != hd.HelpDeskID && hd.ReplyID == replyId);
            List<ResultHelpDeskDto> valueToDto = _mapper.Map<List<ResultHelpDeskDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        public IActionResult AddHelpDesk(CreateHelpDeskDto createHelpDeskDto)
        {
            createHelpDeskDto.IsRead = false;
            createHelpDeskDto.SendDate = DateTime.Now;

            var validatorResult = _createValidator.Validate(createHelpDeskDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            HelpDesk valueFromDto = _mapper.Map<HelpDesk>(createHelpDeskDto);
            IResult values = _helpDeskService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Help Desk Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        public IActionResult UpdateAbout(UpdateHelpDeskDto updateHelpDeskDto)
        {
            var validatorResult = _updateValidator.Validate(updateHelpDeskDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            HelpDesk valueFromDto = _mapper.Map<HelpDesk>(updateHelpDeskDto);
            IResult values = _helpDeskService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Help Desk Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        public IActionResult DeleteAbout(Guid id)
        {
            IDataResult<bool> state = _helpDeskService.TDeleteDataGuid(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Help Desk Verisi Başarıyla Silindi." });
        }
    }
}
