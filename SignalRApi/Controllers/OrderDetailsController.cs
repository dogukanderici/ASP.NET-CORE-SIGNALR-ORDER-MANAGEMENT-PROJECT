using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.OrderDetailDtos;
using SignalR.Entities.Concrete;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderDetailsController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetAllData()
        {
            IDataResult<List<OrderDetail>> values = _orderDetailService.TGetAllData(null, x => x.Product);
            List<ResultOrderDetailDto> valueToDto = _mapper.Map<List<ResultOrderDetailDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetOrderDetailDataById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetOrderDetailDataById(Guid id)
        {
            IDataResult<OrderDetail> values = _orderDetailService.TGetData(x => x.OrderDetailID == id, x => x.Product);
            ResultOrderDetailDto valueToDto = _mapper.Map<ResultOrderDetailDto>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetOrderDetailDataByOrderId")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetOrderDetailDataByOrderId(Guid id)
        {
            IDataResult<List<OrderDetail>> values = _orderDetailService.TGetAllData(x => x.OrderID == id, x => x.Product);
            List<ResultOrderDetailDto> valueToDto = _mapper.Map<List<ResultOrderDetailDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult CreateOrderDetail(CreateOrderDetailDto createOrderDetailDto)
        {
            OrderDetail valueFromDto = _mapper.Map<OrderDetail>(createOrderDetailDto);
            IResult result = _orderDetailService.TAddData(valueFromDto);

            if (result.SuccessCode != System.Net.HttpStatusCode.OK)
            {
                return StatusCode((int)result.SuccessCode, result);
            }

            return Ok(new { success = true, message = "Sipariş Detayı Başarıyla Kaydedildi." });
        }

        [HttpPut]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult UpdateOrderDetail(UpdateOrderDetailDto updateOrderDetailDto)
        {
            OrderDetail valueFromDto = _mapper.Map<OrderDetail>(updateOrderDetailDto);
            IResult result = _orderDetailService.TUpdateData(valueFromDto);

            if (result.SuccessCode != System.Net.HttpStatusCode.OK)
            {
                return StatusCode((int)result.SuccessCode, result);
            }

            return Ok(new { success = true, message = "Sipariş Detayı Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult DeleteOrderDetail(int id)
        {
            IResult result = _orderDetailService.TDeleteData(id);

            if (result.SuccessCode != System.Net.HttpStatusCode.OK)
            {
                return StatusCode((int)result.SuccessCode, result);
            }

            return Ok(new { success = true, message = "Sipariş Detayı Başarıyla Silindi." });
        }
    }
}
