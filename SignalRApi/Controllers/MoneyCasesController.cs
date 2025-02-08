using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Business.Settings;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.MoneyCaseDtos;
using SignalR.Dtos.Dtos.PropertySettingsDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    //[Authorize(Policy = "AdminPermissionPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyCasesController : BaseController
    {
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IMapper _mapper;

        public MoneyCasesController(IMoneyCaseService moneyCaseService, IMapper mapper)
        {
            _moneyCaseService = moneyCaseService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMoneyCase()
        {
            IDataResult<List<MoneyCase>> allDataForCurrentMounth = _moneyCaseService.TGetAllData((mc => mc.OperationDate >= DateTime.Now.AddDays(1 - DateTime.Now.Day).Date && mc.OperationDate <= DateTime.Now), null);

            return Ok(_mapper.Map<List<ResultMoneyCaseDto>>(allDataForCurrentMounth.Data));
        }

        [HttpGet("GetAllOutcomeData")]
        public IActionResult GetAllOutcomeData()
        {
            IDataResult<List<MoneyCase>> allDataForCurrentMounth = _moneyCaseService.TGetAllData(
                (mc => mc.OperationDate >= DateTime.Now.AddDays(1 - DateTime.Now.Day).Date && mc.OperationDate <= DateTime.Now && mc.OperationType == "outcome"), null);

            return Ok(_mapper.Map<List<ResultMoneyCaseDto>>(allDataForCurrentMounth.Data));
        }

        [HttpGet("GetMoneyCaseById")]
        public IActionResult GetMoneyCase(int id)
        {
            IDataResult<MoneyCase> value = _moneyCaseService.TGetData(mc => mc.MoneyCaseID == id);

            return Ok(_mapper.Map<ResultMoneyCaseDto>(value.Data));
        }

        [HttpGet("GetTotalMoneyCaseAmount")]
        public IActionResult GetTotalMoneyCaseAmount()
        {
            IDataResult<List<PropertySettings>> value = _moneyCaseService.TGetTotalMoneyCaseAmount();

            return Ok(_mapper.Map<List<PropertySettingsDto>>(value.Data));
        }

        [HttpPost]
        public IActionResult CreateMoneyCase(CreateMoneyCaseDto createMoneyCaseDto)
        {
            MoneyCase valueFromDto = _mapper.Map<MoneyCase>(createMoneyCaseDto);

            IResult values = _moneyCaseService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Kasa Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        public IActionResult UpdateMoneyCase(UpdateMoneyCaseDto updateMoneyCaseDto)
        {
            MoneyCase valueFromDto = _mapper.Map<MoneyCase>(updateMoneyCaseDto);

            IResult values = _moneyCaseService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Kasa Verisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        public IActionResult DeleteMoneyCase(int id)
        {
            IDataResult<bool> state = _moneyCaseService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Kasa Verisi Başarıyla Silindi." });
        }
    }
}
