using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.Discounts;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : BaseController
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDiscountDto> _createValidator;
        private readonly IValidator<UpdateDiscountDto> _updateValidator;

        public DiscountsController(IDiscountService discountService, IMapper mapper, IValidator<CreateDiscountDto> createValidator, IValidator<UpdateDiscountDto> updateValidator)
        {
            _discountService = discountService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetAllData()
        {
            IDataResult<List<Discount>> values = _discountService.TGetAllData();
            List<ResultDiscountDto> valuesToDto = _mapper.Map<List<ResultDiscountDto>>(values.Data);

            return Ok(valuesToDto);
        }

        [HttpGet("GetDiscountById")]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetData(Guid id)
        {
            IDataResult<Discount> values = _discountService.TGetData(d => d.DiscountID == id, null);
            ResultDiscountDto valuesToDto = _mapper.Map<ResultDiscountDto>(values.Data);

            return Ok(valuesToDto);
        }

        [HttpGet("GetDiscountByTitle")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetDataByTitle(string title)
        {
            IDataResult<Discount> values = _discountService.TGetData(d => d.Title == title, null);
            ResultDiscountDto valuesToDto = _mapper.Map<ResultDiscountDto>(values.Data);

            return Ok(valuesToDto);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult CreateDiscount(CreateDiscountDto createDiscountDto)
        {
            var validatorResult = _createValidator.Validate(createDiscountDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Discount valueFromDto = _mapper.Map<Discount>(createDiscountDto);

            IResult values = _discountService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "İndirim Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            var validatorResult = _updateValidator.Validate(updateDiscountDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Discount valueFromDto = _mapper.Map<Discount>(updateDiscountDto);

            IResult values = _discountService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "İndirim Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult DeleteDiscount(Guid id)
        {
            IResult values = _discountService.TDeleteDataByGuid(id);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "İndirim Başarıyla Silindi." });
        }
    }
}
