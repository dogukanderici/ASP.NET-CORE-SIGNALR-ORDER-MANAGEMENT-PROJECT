using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SignalR.Dtos.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.SendMailDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Controllers
{
    [Route("BookATable")]
    public class BookATableController : Controller
    {
        private readonly IBookingServicePS _bookingServicePS;
        private readonly ISendMailServicePS _sendMailServicePS;

        public BookATableController(IBookingServicePS bookingServicePS, ISendMailServicePS sendMailServicePS)
        {
            _bookingServicePS = bookingServicePS;
            _sendMailServicePS = sendMailServicePS;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("IndexBooking")]
        public IActionResult Index(string defaultBookingViewModelJson)
        {
            DefaultBookingViewModel model = JsonConvert.DeserializeObject<DefaultBookingViewModel>(defaultBookingViewModelJson);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(DefaultBookingViewModel defaultBookingViewModel)
        {
            var response = await _bookingServicePS.CreateDataAsync(defaultBookingViewModel.CreateBookingData);

            defaultBookingViewModel.HttpResponseMessage = response.StatusMessage;
            defaultBookingViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return RedirectToAction("IndexBooking", "BookATable", new { defaultBookingViewModelJson = JsonConvert.SerializeObject(defaultBookingViewModel) });
        }
    }
}
