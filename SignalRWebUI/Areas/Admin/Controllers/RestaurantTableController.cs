using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SignalR.Core.Utilities.Handlers;
using SignalR.Dtos.Dtos.RestaurantTableDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDtos;
using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/RestaurantTable")]
    public class RestaurantTableController : AdminBaseController
    {

        private readonly IRestaurantTableServicePS _restaurantTableService;
        private readonly IOrderServicePS _orderService;
        private readonly IMapper _mapper;
        private readonly IQRCodeGeneratorHandler _generatorQRCode;
        private readonly ApiSettings _apiSettings;
        private readonly IMemoryCache _memoryCache;

        public RestaurantTableController(IRestaurantTableServicePS restaurantTableServicePS, IMapper mapper, IQRCodeGeneratorHandler generatorQRCode, IOptions<ApiSettings> apiSettings, IMemoryCache memoryCache, IOrderServicePS orderService)
        {
            _restaurantTableService = restaurantTableServicePS;
            _mapper = mapper;
            _generatorQRCode = generatorQRCode;
            _apiSettings = apiSettings.Value;
            _memoryCache = memoryCache;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Masalar";
            ViewBag.EmptyDataInfo = "Masa Bilgisi";

            var response = await _restaurantTableService.GetAllDataAsync(null);

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

        [HttpGet]
        [Route("TableStatus")]
        public async Task<IActionResult> TableStatus()
        {
            ViewBag.MainTitle = "Masalar";
            ViewBag.EmptyDataInfo = "Masa Bilgisi";
            ViewBag.ApiBaseUrl = _apiSettings.ApiBaseUrl + "SignalRHub";
            ViewBag.UserAccessToken = HttpContext.Request.Cookies["SignalRCookie"];

            var response = await _restaurantTableService.GetAllDataAsync(null);

            RestaurantTableViewModel model = new RestaurantTableViewModel();

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

        [HttpGet]
        [Route("TableOrderDetail")]
        public async Task<IActionResult> TableOrderDetail(string tableName)
        {
            ViewBag.MainTitle = "Masalar";
            ViewBag.EmptyDataInfo = "Masa Bilgisi";

            var response = await _orderService.GetDataByTableAsync(tableName);

            OrderViewModel model = new OrderViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = response.StatusMessage;
            }
            else
            {
                ResultAdminOrderDto values = _mapper.Map<ResultAdminOrderDto>(response.GetData);
                model.OrderData = values;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateRestaurantTable()
        {
            ViewBag.MainTitle = "Masalar";

            RestaurantTableViewModel model = new RestaurantTableViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateRestaurantTable(RestaurantTableViewModel restaurantTableViewModel)
        {
            restaurantTableViewModel.CreateRestaurantTable.IsActive = true;
            restaurantTableViewModel.CreateRestaurantTable.Status = false;

            string qrCodeDataForTable = "https:" + "//localhost:" + "7169/default/indexwithtable" + "?id=" + restaurantTableViewModel.CreateRestaurantTable.Name;

            string qrCodeData = _generatorQRCode.CreateQRCode(qrCodeDataForTable, 10);

            restaurantTableViewModel.CreateRestaurantTable.QRCodeForTable = qrCodeData;

            var response = await _restaurantTableService.CreateDataAsync(restaurantTableViewModel.CreateRestaurantTable);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.ResponseMessage = $"Yeni Masa Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

                restaurantTableViewModel.HttpResponseMessage = response.StatusMessage;
                restaurantTableViewModel.ApiResponseMessage = response.ApiResponseMessage;
            }

            return View(restaurantTableViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateRestaurantTable(int id)
        {
            ViewBag.MainTitle = "Masalar";

            var responseMessage = await _restaurantTableService.GetDataAsync(id);

            var model = new RestaurantTableViewModel();

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                UpdateAdminRestaurantTableDto values = _mapper.Map<UpdateAdminRestaurantTableDto>(responseMessage.GetData);
                model.UpdateRestaurant = values;
            }
            else
            {
                ViewBag.ResponseMessage = $"Masalar Listelenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id})")]
        public async Task<IActionResult> UpdateRestaurantTable(RestaurantTableViewModel restaurantTableViewModel)
        {
            // Masaya Ait Yeni Bir QR Kod Oluşturur.

            if (restaurantTableViewModel.UpdateRestaurant.QRCodeForTable == null ||
                restaurantTableViewModel.UpdateRestaurant.QRCodeForTable == "" ||
                restaurantTableViewModel.CreateNewQRCodeForUpdate)
            {
                string qrCodeDataForTable = "https:" + "//localhost:" + "7169/default/indexwithtable" + "?id=" + restaurantTableViewModel.UpdateRestaurant.Name;
                string qrCodeData = _generatorQRCode.CreateQRCode(qrCodeDataForTable, 10);
                restaurantTableViewModel.UpdateRestaurant.QRCodeForTable = qrCodeData;
            }

            var response = await _restaurantTableService.UpdateDataAsync(restaurantTableViewModel.UpdateRestaurant);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            restaurantTableViewModel.HttpResponseMessage = response.StatusMessage;
            restaurantTableViewModel.ApiResponseMessage = response.ApiResponseMessage;

            ViewBag.ResponseMessage = $"Masa Bilgisi Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            return View(restaurantTableViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteRestaurantTable(int id)
        {
            await _restaurantTableService.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
