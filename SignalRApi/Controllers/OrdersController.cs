using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.OrderDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetAllData()
        {
            IDataResult<List<Order>> result = _orderService.TGetAllData(null, o => o.OrderDetails);
            List<ResultOrderDto> valueToDto = _mapper.Map<List<ResultOrderDto>>(result.Data);

            return Ok(valueToDto);

        }

        [HttpGet("GetOrderById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetAllData(Guid id)
        {
            IDataResult<Order> result = _orderService.TGetData(o => o.OrderID == id, o => o.OrderDetails);
            ResultOrderDto valueToDto = _mapper.Map<ResultOrderDto>(result.Data);

            return Ok(valueToDto);

        }

        [HttpGet("GetOrderByTable")]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetOrderByTable(string id)
        {
            //IDataResult<Order> result = _orderService.TGetData(o => o.Masa == id && o.OrderStatus == false, o => o.OrderDetails);
            //ResultOrderDto valueToDto = _mapper.Map<ResultOrderDto>(result.Data);

            //return Ok(valueToDto);

            IDataResult<Order> result = _orderService.TGetDataWithProduct(o => o.Masa == id && o.OrderStatus == false);
            ResultOrderDto valueToDto = _mapper.Map<ResultOrderDto>(result.Data);

            return Ok(valueToDto);

        }

        //[HttpGet("GetOrderByTableWithProduct")]
        //[Authorize(Policy = "AdminPermissionPolicy")]
        //public IActionResult GetOrderByTableWithProduct(string id)
        //{
        //    IDataResult<Order> result = _orderService.TGetDataWithProduct(o => o.Masa == id && o.OrderStatus == false, o => o.OrderDetails, );
        //    ResultOrderDto valueToDto = _mapper.Map<ResultOrderDto>(result.Data);

        //    return Ok(valueToDto);

        //}

        [HttpPost]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult CreateOrder(CreateOrderDto createOrderDto)
        {
            Order valueFromDto = _mapper.Map<Order>(createOrderDto);

            IResult result = _orderService.TAddData(valueFromDto);

            if (result.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)result.SuccessCode, result);
            }

            return Ok(new { success = true, message = "Sipariş Başarıyla Kaydedildi!" });
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            Order valueFromDto = _mapper.Map<Order>(updateOrderDto);

            IResult result = _orderService.TUpdateData(valueFromDto);

            if (result.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)result.SuccessCode, result);
            }

            return Ok(new { success = true, message = "Sipariş Başarıyla Güncellendi!" });
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult DeleteOrder(Guid id)
        {
            IDataResult<bool> state = _orderService.TDeleteDataByGuid(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Sipariş Başarıyla Silindi." });
        }
    }
}
