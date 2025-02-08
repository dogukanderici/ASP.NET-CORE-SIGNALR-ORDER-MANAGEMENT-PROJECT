using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.RestaurantTableDtos;
using SignalR.Entities.Concrete;
using System.Collections.Generic;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantTablesController : BaseController
    {
        private readonly IRestaurantTableService _restaurantTableService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateRestaurantTableDto> _createValidator;
        private readonly IValidator<UpdateRestaurantTableDto> _updateValidator;

        public RestaurantTablesController(IRestaurantTableService restaurantTableService, IMapper mapper, IValidator<CreateRestaurantTableDto> createValidator, IValidator<UpdateRestaurantTableDto> updateValidator)
        {
            _restaurantTableService = restaurantTableService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetAllTable()
        {
            IDataResult<List<RestaurantTable>> values = _restaurantTableService.TGetAllData();
            List<ResultRestaurantTableDto> valueToDto = _mapper.Map<List<ResultRestaurantTableDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetTableById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetTableById(int id)
        {
            IDataResult<RestaurantTable> values = _restaurantTableService.TGetData(t => t.RestaurantTableID == id);
            ResultRestaurantTableDto valueToDto = _mapper.Map<ResultRestaurantTableDto>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetTableIdByTableName")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetTableIdByTableName(string tableName)
        {
            IDataResult<RestaurantTable> values = _restaurantTableService.TGetData(t => t.Name == tableName);
            ResultRestaurantTableDto valueToDto = _mapper.Map<ResultRestaurantTableDto>(values.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult CreateTable(CreateRestaurantTableDto createRestaurantTableDto)
        {
            var validatorResult = _createValidator.Validate(createRestaurantTableDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            RestaurantTable valueFromDto = _mapper.Map<RestaurantTable>(createRestaurantTableDto);
            IResult value = _restaurantTableService.TAddData(valueFromDto);

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)value.SuccessCode, value.Message);
            }

            return Ok(new { success = true, message = "Masa Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult UpdateTable(UpdateRestaurantTableDto updateRestaurantTableDto)
        {
            var validatorResult = _updateValidator.Validate(updateRestaurantTableDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            RestaurantTable valueFromDto = _mapper.Map<RestaurantTable>(updateRestaurantTableDto);
            IResult value = _restaurantTableService.TUpdateData(valueFromDto);

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)value.SuccessCode, value.Message);
            }

            return Ok(new { success = true, message = "Masa Verisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult DeleteTable(int id)
        {
            IDataResult<bool> state = _restaurantTableService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Masa Verisi Başarıyla Silindi." });
        }

    }
}
