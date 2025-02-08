using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Areas.User.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/Booking")]
    public class BookingController : UserBaseController
    {
        private readonly IBookingServicePS _bookingServicePS;

        public BookingController(IBookingServicePS bookingServicePS)
        {
            _bookingServicePS = bookingServicePS;
        }

        public async Task<IActionResult> Index()
        {
            UserBookingViewModel model = new UserBookingViewModel();

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                string email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value/*"dogukan.derici@gmail.com"*/;
                string namesurname = User.Claims.FirstOrDefault(c => c.Type == "namesurname")?.Value;
                string username = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

                ViewBag.MainTitle = $"{namesurname} Kullanıcısına Ait Rezervasyonlar";
                ViewBag.EmptyDataInfo = $"Henüz Kayıtlı Bir Rezervasyonunuz Bulunmuyor!";

                var response = await _bookingServicePS.GetAllDataByUserAsync(email);

                if (response.StatusMessage == HttpStatusCode.OK)
                {
                    model.BookingListData = response.GetData;
                }
                else
                {
                    model.HttpResponseMessage = response.StatusMessage;
                    model.ApiResponseMessage = response.ApiResponseMessage;
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateNewBooking()
        {
            ViewBag.MainTitle = "Yeni Rezervasyon Oluşturma";

            UserBookingViewModel model = new UserBookingViewModel();

            string email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            string namesurname = User.Claims.FirstOrDefault(c => c.Type == "namesurname")?.Value;

            model.CreateBookingData = new CreateAdminBookingDto();

            DateTime currentDate = DateTime.Now;

            model.CreateBookingData.Name = namesurname;
            model.CreateBookingData.Mail = email;
            model.CreateBookingData.Date = currentDate.Date.AddHours(0).AddMinutes(0);

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateNewBooking(UserBookingViewModel userBookingViewModel)
        {
            var values = await _bookingServicePS.CreateDataAsync(userBookingViewModel.CreateBookingData);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            userBookingViewModel.HttpResponseMessage = values.StatusMessage;
            userBookingViewModel.ApiResponseMessage = values.ApiResponseMessage;

            return View(userBookingViewModel);
        }
    }
}
