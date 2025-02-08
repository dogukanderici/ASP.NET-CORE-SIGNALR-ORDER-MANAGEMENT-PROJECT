using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.SocialMediaDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSocialMediaDto> _createValidator;
        private readonly IValidator<UpdateSocialMediaDto> _updateValidator;

        public SocialMediaController(ISocialMediaService socialMediaService, IMapper mapper, IValidator<CreateSocialMediaDto> createValidator, IValidator<UpdateSocialMediaDto> updateValidator)
        {
            _socialMediaService = socialMediaService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult SocialMediaList()
        {
            IDataResult<List<SocialMedia>> values = _socialMediaService.TGetAllData();
            List<ResultSocialMediaDto> valueToDto = _mapper.Map<List<ResultSocialMediaDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetSocialMediaById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetSocialMediaById(int id)
        {
            IDataResult<SocialMedia> value = _socialMediaService.TGetData(x => x.SocialMediaID == id);
            ResultSocialMediaDto valueToDto = _mapper.Map<ResultSocialMediaDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult AddSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            var validatorResult = _createValidator.Validate(createSocialMediaDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            SocialMedia valueFromDto = _mapper.Map<SocialMedia>(createSocialMediaDto);
            IResult values = _socialMediaService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Sosyal Medya Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var validatorResult = _updateValidator.Validate(updateSocialMediaDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            SocialMedia valueFromDto = _mapper.Map<SocialMedia>(updateSocialMediaDto);
            IResult values = _socialMediaService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Sosyal Medya Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult DeleteSocialMedia(int id)
        {
            IDataResult<bool> state = _socialMediaService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Sosyal Medya Verisi Başarıyla Silindi." });
        }
    }
}
