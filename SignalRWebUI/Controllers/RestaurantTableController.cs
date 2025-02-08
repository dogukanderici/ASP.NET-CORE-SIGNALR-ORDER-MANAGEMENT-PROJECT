using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Dtos.Dtos.RestaurantTableDtos;
using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Controllers
{
    public class RestaurantTableController : Controller
    {
        private readonly IRestaurantTableServicePS _restaurantTableServicePS;
        private readonly IMapper _mapper;

        public RestaurantTableController(IRestaurantTableServicePS restaurantTableServicePS, IMapper mapper)
        {
            _restaurantTableServicePS = restaurantTableServicePS;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _restaurantTableServicePS.GetAllDataAsync(null);

            var model = new RestaurantTableViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = response.StatusMessage;
            }
            else
            {
                List<ResultAdminRestaurantTableDto> values = _mapper.Map<List<ResultAdminRestaurantTableDto>>(response.GetData);
                model.ResultRestaurants = values;
            }

            return View(model);
        }

        public IActionResult RedirectDefaultWithTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("IndexWithTable", "Default", new { id = tableName });
        }
    }
}
