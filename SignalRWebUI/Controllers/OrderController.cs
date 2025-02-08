using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Areas.Admin.Dtos.FeatureDtos;
using SignalRWebUI.Areas.Admin.Dtos.NotificationDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDetailDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDtos;
using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServicePS _orderServicePS;
        private readonly IOrderDetailServicePS _orderDetailServicePS;
        private readonly IBasketServicePS _basketServicePS;
        private readonly IRestaurantTableServicePS _restaurantTableServicePS;
        private readonly INotificationServicePS _notificationServicePS;

        public OrderController(IOrderServicePS orderServicePS, IOrderDetailServicePS orderDetailServicePS, IBasketServicePS basketServicePS, IRestaurantTableServicePS restaurantTableServicePS, INotificationServicePS notificationServicePS)
        {
            _orderServicePS = orderServicePS;
            _orderDetailServicePS = orderDetailServicePS;
            _basketServicePS = basketServicePS;
            _restaurantTableServicePS = restaurantTableServicePS;
            _notificationServicePS = notificationServicePS;
        }

        [HttpPost]
        public async Task<IActionResult> SendOrder(string BasketViewModelJson)
        {
            BasketViewModel model = JsonConvert.DeserializeObject<BasketViewModel>(BasketViewModelJson);

            CreateAdminOrderDto createAdminOrderDto = new CreateAdminOrderDto();
            List<CreateAdminOrderDetailDto> createAdminOrderDetailDtoList = new List<CreateAdminOrderDetailDto>();
            UpdateAdminRestaurantTableDto updateAdminRestaurantTableDto = new UpdateAdminRestaurantTableDto();
            CreateAdminNotificationDto createAdminNotificationDto = new CreateAdminNotificationDto();

            // CreateOrderDto
            createAdminOrderDto.OrderID = Guid.NewGuid();
            createAdminOrderDto.Masa = HttpContext.Request.Cookies["MasaID"];
            createAdminOrderDto.OrderDate = DateTime.Now;
            createAdminOrderDto.TotalPrice = model.TotalBasketPriceWithDiscount;
            createAdminOrderDto.Description = "Yeni Sipariş";
            createAdminOrderDto.OrderStatus = false;

            var responseOrder = await _orderServicePS.CreateDataAsync(createAdminOrderDto);

            // CreateAdminNotificationDto
            createAdminNotificationDto.Date = DateTime.Now;
            createAdminNotificationDto.Description = $"Yeni Sipariş-Masa {createAdminOrderDto.Masa}";
            createAdminNotificationDto.Status = false;
            createAdminNotificationDto.Type = "booking";

            var notificationResponse = await _notificationServicePS.CreateDataAsync(createAdminNotificationDto);

            if ((responseOrder != null) && (responseOrder.StatusMessage == HttpStatusCode.OK))
            {
                model.BasketListData.ForEach((basketData) =>
                {
                    // CreateOrderDetailDto
                    CreateAdminOrderDetailDto createAdminOrderDetailDto = new CreateAdminOrderDetailDto();

                    createAdminOrderDetailDto.OrderDetailID = Guid.NewGuid();
                    createAdminOrderDetailDto.OrderID = createAdminOrderDto.OrderID;
                    createAdminOrderDetailDto.ProductID = basketData.ProductID;
                    createAdminOrderDetailDto.ProductCount = Convert.ToInt32(basketData.Count);
                    createAdminOrderDetailDto.UnitPrice = basketData.Price;
                    createAdminOrderDetailDto.TotalPrice = basketData.TotalPrice;
                    createAdminOrderDetailDto.OrderStatus = false;

                    updateAdminRestaurantTableDto.RestaurantTableID = basketData.RestaurantTableID;

                    createAdminOrderDetailDtoList.Add(createAdminOrderDetailDto);
                });

                foreach (var item in createAdminOrderDetailDtoList)
                {
                    var responseOrderDetail = await _orderDetailServicePS.CreateDataAsync(item);
                }

                int tableId = updateAdminRestaurantTableDto.RestaurantTableID;

                await _basketServicePS.DeleteDataAsync(tableId);

                var data = await _restaurantTableServicePS.GetDataAsync(tableId);

                updateAdminRestaurantTableDto.Name = data.GetData.Name;
                updateAdminRestaurantTableDto.PersonCount = data.GetData.PersonCount;
                updateAdminRestaurantTableDto.Status = true;
                updateAdminRestaurantTableDto.IsActive = true;
                updateAdminRestaurantTableDto.QRCodeForTable = data.GetData.QRCodeForTable;

                var responseTable = await _restaurantTableServicePS.UpdateDataAsync(updateAdminRestaurantTableDto);
            }

            return RedirectToAction("", "Baskets");
        }
    }
}
