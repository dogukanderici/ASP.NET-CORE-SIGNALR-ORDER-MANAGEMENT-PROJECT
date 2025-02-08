using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.DailyDiscountDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyDiscountController : ControllerBase
    {
        private readonly IDailyDiscountService _dailyDiscountService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDailyDiscountDto> _createValidator;
        private readonly IValidator<UpdateDailyDiscountDto> _updateValidator;

        public DailyDiscountController(IDailyDiscountService dailyDiscountService, IMapper mapper, IValidator<CreateDailyDiscountDto> createValidator, IValidator<UpdateDailyDiscountDto> updateValidator)
        {
            _dailyDiscountService = dailyDiscountService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult DailyDiscountList()
        {
            IDataResult<List<DailyDiscount>> values = _dailyDiscountService.TGetAllData();
            List<ResultDailyDiscountDto> valueToDto = _mapper.Map<List<ResultDailyDiscountDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetDailyDiscountById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetDailyDiscountById(int id)
        {
            IDataResult<DailyDiscount> value = _dailyDiscountService.TGetData(x => x.DailyDiscountID == id);
            ResultDailyDiscountDto valueToDto = _mapper.Map<ResultDailyDiscountDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult AddDailyDiscount(CreateDailyDiscountDto createDailyDiscountDto)
        {
            var validatorResult = _createValidator.Validate(createDailyDiscountDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            DailyDiscount valueFromDto = _mapper.Map<DailyDiscount>(createDailyDiscountDto);
            IResult values = _dailyDiscountService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Günlük İndirim Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateDailyDiscount(UpdateDailyDiscountDto updateDailyDiscountDto)
        {
            var validatorResult = _updateValidator.Validate(updateDailyDiscountDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            DailyDiscount valueFromDto = _mapper.Map<DailyDiscount>(updateDailyDiscountDto);
            IResult values = _dailyDiscountService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Günlük İndirim Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteDailyDiscount(int id)
        {
            IDataResult<bool> state = _dailyDiscountService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Günlük İndirim Verisi Başarıyla Silindi." });
        }
    }
}
