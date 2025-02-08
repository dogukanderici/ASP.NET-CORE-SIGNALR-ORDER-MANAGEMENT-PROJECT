using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.BasketDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBasketDto> _createValidator;

        public BasketController(IBasketService basketService, IMapper mapper, IValidator<CreateBasketDto> createValidator)
        {
            _basketService = basketService;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetBasket(string restaurantTableID)
        {
            int restaurantTableId = Convert.ToInt32(restaurantTableID);

            IDataResult<List<Basket>> values = _basketService.TGetAllData(x => x.RestaurantTableID == restaurantTableId, x => x.Product);
            List<ResultBasketDto> valuesToDto = _mapper.Map<List<ResultBasketDto>>(values.Data);

            return Ok(valuesToDto);
        }

        [HttpGet("GetBasketByTableID")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetBasketByTableID(int id)
        {
            IDataResult<List<Basket>> values = _basketService.TGetBasketByTableNumber(x => x.Product, id);
            List<ResultBasketDto> valuesToDto = _mapper.Map<List<ResultBasketDto>>(values.Data);

            return Ok(valuesToDto);
        }

        [HttpPost]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult AddItemBasket(CreateBasketDto createBasketDto)
        {
            var validatorResult = _createValidator.Validate(createBasketDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Basket valueFromDto = _mapper.Map<Basket>(createBasketDto);
            IResult value = _basketService.TAddData(valueFromDto);

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)value.SuccessCode, value);
            }

            return Ok(new { success = true, message = "Ürün Sepete Eklendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult DeleteBasket(int id)
        {
            IResult value = _basketService.TDeleteBasket(id);

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)value.SuccessCode, value);
            }

            return Ok(new { success = true, message = "Ürün Sepetinizden Kaldırıldı!" });
        }

        [HttpDelete("DeleteBasketItem")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult DeleteBasketItem(int id, int tableID)
        {
            IResult value = _basketService.TDeleteBasketItem(id, tableID);

            if (value.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)value.SuccessCode, value);
            }

            return Ok(new { success = true, message = "Ürün Sepetinizden Kaldırıldı!" });
        }
    }
}
