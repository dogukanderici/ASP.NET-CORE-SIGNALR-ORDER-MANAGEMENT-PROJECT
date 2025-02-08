using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.WorkingHourDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingHourController : ControllerBase
    {
        private readonly IWorkingHourService _workingHourService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateWorkingHourDto> _createValidator;
        private readonly IValidator<UpdateWorkingHourDto> _updateValidator;

        public WorkingHourController(IWorkingHourService workingHourService, IMapper mapper, IValidator<CreateWorkingHourDto> createValidator, IValidator<UpdateWorkingHourDto> updateValidator)
        {
            _workingHourService = workingHourService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult WorkingHourList()
        {
            IDataResult<List<WorkingHour>> values = _workingHourService.TGetAllData();

            if (values.SuccessCode == HttpStatusCode.OK)
            {
                List<ResultWorkingHourDto> valueToDto = _mapper.Map<List<ResultWorkingHourDto>>(values.Data);

                return Ok(valueToDto);
            }

            return StatusCode((int)values.SuccessCode, values);
        }

        [HttpGet("GetWorkingHourById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetWorkingHourById(int id)
        {
            IDataResult<WorkingHour> value = _workingHourService.TGetData(x => x.WorkingHourID == id);

            if (value == null)
            {
                return NotFound(new { success = false, message = "Çalışma Saati Bulunamadı" });
            }

            ResultWorkingHourDto valueToDto = _mapper.Map<ResultWorkingHourDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult AddWorkingHour(CreateWorkingHourDto createWorkingHourDto)
        {
            var validatorResult = _createValidator.Validate(createWorkingHourDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            WorkingHour valueFromDto = _mapper.Map<WorkingHour>(createWorkingHourDto);
            IResult values = _workingHourService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Çalışma Saatleri Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult UpdateWorkingHour(UpdateWorkingHourDto updateWorkingHourDto)
        {
            var validatorResult = _updateValidator.Validate(updateWorkingHourDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            WorkingHour valueFromDto = _mapper.Map<WorkingHour>(updateWorkingHourDto);
            IResult values = _workingHourService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Çalışma Saatleri Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult DeleteWorkingHour(int id)
        {
            IDataResult<bool> state = _workingHourService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Çalışma Saatleri Verisi Başarıyla Silindi." });
        }
    }
}
