using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Areas.Admin.Dtos.OrderDtos;
using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;
using SignalRWebUI.Services.Abstract;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Payment")]
    public class PaymentController : AdminBaseController
    {
        private readonly IOrderServicePS _orderServicePS;
        private readonly IRestaurantTableServicePS _restaurantTableServicePS;
        private readonly IMapper _mapper;

        public PaymentController(IOrderServicePS orderServicePS, IRestaurantTableServicePS restaurantTableServicePS, IMapper mapper)
        {
            _orderServicePS = orderServicePS;
            _restaurantTableServicePS = restaurantTableServicePS;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string masaID)
        {

            var orderResponse = await _orderServicePS.GetDataByTableAsync(masaID);
            var tableResponse = await _restaurantTableServicePS.GetDataAsync(Convert.ToInt32(masaID));

            // Siapriş Bilgisi Güncellenir.
            UpdateAdminOrderDto updateAdminOrderDto = _mapper.Map<UpdateAdminOrderDto>(orderResponse.GetData);
            updateAdminOrderDto.OrderStatus = true;

            // Masa Durumu Güncellenir.
            UpdateAdminRestaurantTableDto updateAdminRestaurantTableDto = _mapper.Map<UpdateAdminRestaurantTableDto>(tableResponse.GetData);
            updateAdminRestaurantTableDto.Status = false;


            await _orderServicePS.UpdateDataAsync(updateAdminOrderDto);
            await _restaurantTableServicePS.UpdateDataAsync(updateAdminRestaurantTableDto);

            return RedirectToAction("TableStatus", "RestaurantTable", new { area = "Admin" });
        }
    }
}
