using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.FeatureDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFeatureDto> _createValidator;
        private readonly IValidator<UpdateFeatureDto> _updateValidator;

        public FeatureController(IFeatureService featureService, IMapper mapper, IValidator<CreateFeatureDto> createValidator, IValidator<UpdateFeatureDto> updateValidator)
        {
            _featureService = featureService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult FeatureList()
        {
            IDataResult<List<Feature>> values = _featureService.TGetAllData();
            List<ResultFeatureDto> valueToDto = _mapper.Map<List<ResultFeatureDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetFeatureById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetFeatureById(int id)
        {
            IDataResult<Feature> value = _featureService.TGetData(x => x.FeatureID == id);
            ResultFeatureDto valueToDto = _mapper.Map<ResultFeatureDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult AddFeature(CreateFeatureDto createFeatureDto)
        {
            var validatorResult = _createValidator.Validate(createFeatureDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Feature valueFromDto = _mapper.Map<Feature>(createFeatureDto);
            IResult values = _featureService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Öne Çıkan Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var validatorResult = _updateValidator.Validate(updateFeatureDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Feature valueFromDto = _mapper.Map<Feature>(updateFeatureDto);
            _featureService.TUpdateData(valueFromDto);

            return Ok(new { success = true, message = "Öne Çıkan Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteFeature(int id)
        {
            IDataResult<bool> state = _featureService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Öne Çıkan Verisi Başarıyla Silindi." });
        }
    }
}
