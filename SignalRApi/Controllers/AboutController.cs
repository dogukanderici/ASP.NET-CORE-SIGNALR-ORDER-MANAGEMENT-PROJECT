using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Business.Utilities.Validations.AboutValidations;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.AboutDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : BaseController
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAboutDto> _createValidator;
        private readonly IValidator<UpdateAboutDto> _updateValidator;

        public AboutController(IAboutService aboutService, IMapper mapper, IValidator<CreateAboutDto> createValidator, IValidator<UpdateAboutDto> updateValidator)
        {
            _aboutService = aboutService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult AboutList()
        {
            IDataResult<List<About>> values = _aboutService.TGetAllData(null);
            List<ResultAboutDto> valueToDto = _mapper.Map<List<ResultAboutDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetAboutById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetAboutById(int id)
        {
            IDataResult<About> value = _aboutService.TGetData(x => x.AboutID == id);
            ResultAboutDto valueToDto = _mapper.Map<ResultAboutDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult AddAbout(CreateAboutDto createAboutDto)
        {
            var validationResult = _createValidator.Validate(createAboutDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            About valueFromDto = _mapper.Map<About>(createAboutDto);
            IResult values = _aboutService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Hakkımda Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var validationResult = _updateValidator.Validate(updateAboutDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            About valueFromDto = _mapper.Map<About>(updateAboutDto);
            IResult values = _aboutService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Hakkımda Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteAbout(int id)
        {
            IDataResult<bool> state = _aboutService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Hakkımda Verisi Başarıyla Silindi." });
        }
    }
}
