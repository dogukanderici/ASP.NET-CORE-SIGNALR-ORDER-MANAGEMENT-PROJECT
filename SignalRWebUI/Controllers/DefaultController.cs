using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos;
using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHelpDeskServicePS _helpDeskServicePS;
        private readonly IRestaurantTableServicePS _restaurantTableServicePS;
        private readonly IMapper _mapper;

        public DefaultController(IHelpDeskServicePS helpDeskServicePS, IRestaurantTableServicePS restaurantTableServicePS, IMapper mapper)
        {
            _helpDeskServicePS = helpDeskServicePS;
            _restaurantTableServicePS = restaurantTableServicePS;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> IndexWithTable(string id)
        {
            var response = await _restaurantTableServicePS.GetTableIdByTableName(id);

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                ViewBag.HttpResponseMessage = response.StatusMessage;
            }
            else
            {
                ResultAdminRestaurantTableDto values = _mapper.Map<ResultAdminRestaurantTableDto>(response.GetData);

                HttpContext.Response.Cookies.Append("MasaID", Convert.ToString(values.RestaurantTableID), new CookieOptions
                {
                    HttpOnly = true
                });
            }

            return RedirectToAction("Index", "Default");
        }

        public IActionResult Index()
        {
            string masaId = HttpContext.Request.Cookies["MasaID"];

            if (string.IsNullOrEmpty(masaId))
            {
                return RedirectToAction("Index", "RestaurantTable");
            }

            ViewBag.MasaID = HttpContext.Request.Cookies["MasaID"];

            return View();
        }

        [HttpGet]
        public PartialViewResult SendHelpDesk()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendHelpDesk(CreateAdminHelpDeskDto createAdminHelpDeskDto)
        {
            createAdminHelpDeskDto.HelpDeskID = Guid.NewGuid();
            createAdminHelpDeskDto.ReplyID = createAdminHelpDeskDto.HelpDeskID;

            var values = await _helpDeskServicePS.CreateDataAsync(createAdminHelpDeskDto);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            return View(createAdminHelpDeskDto);
        }
    }
}
