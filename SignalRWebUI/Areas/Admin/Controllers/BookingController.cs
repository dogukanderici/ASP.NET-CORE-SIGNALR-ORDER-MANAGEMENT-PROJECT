using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.Dtos.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.SendMailDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Booking")]
    public class BookingController : AdminBaseController
    {
        private readonly IBookingServicePS _bookingServicePS;
        private readonly IMapper _mapper;
        private readonly ISendMailServicePS _sendMailServicePS;

        public BookingController(IBookingServicePS bookingServicePS, IMapper mapper, ISendMailServicePS sendMailServicePS)
        {
            _bookingServicePS = bookingServicePS;
            _mapper = mapper;
            _sendMailServicePS = sendMailServicePS;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Rezervasyonlar";
            ViewBag.EmptyDataInfo = "Rezervasyon Bilgisi";

            var responseMessage = await _bookingServicePS.GetAllDataAsync(null);

            var model = new BookingViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                model.ResultBookings = _mapper.Map<List<ResultAdminBookingDto>>(responseMessage.GetData);
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateBooking()
        {
            ViewBag.MainTitle = "Rezervasyonlar";

            var model = new BookingViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateBooking(BookingViewModel bookingViewModel)
        {
            var response = await _bookingServicePS.CreateDataAsync(bookingViewModel.CreateBooking);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                // Yeni rezervasyon oluşturulduğunda müşteriye rezervasyon bilgileri mail olarak gönderilir.
                string bookingDetail = $@"
                <ul>
                    <li><strong>Tarih:</strong> {bookingViewModel.CreateBooking.Date}</li>
                    <li><strong>Ad / Soyad:</strong> {bookingViewModel.CreateBooking.Name}</li>
                    <li><strong>Kişi Sayısı:</strong> {bookingViewModel.CreateBooking.PersonCount}</li>
                    <li><strong>Telefon:</strong> {bookingViewModel.CreateBooking.Phone}</li>
                </ul>";


                string mailBody = $@"<p>Merhaba {bookingViewModel.CreateBooking.Name}</p>
                    <p><strong>{bookingViewModel.CreateBooking.Date}</strong> Tarihli Rezervasyonunuz Oluşturulmuştur.</p>
                    <p>Rezervasyon bilgilerinize aşağıdan ulaşabilirsiniz.</p>
                    {bookingDetail}
                    <p>Siz değerli müşterimizi en lezzetli yemeklerimizle ağırlamak için sabırsızlanıyoruz.</p>
                    </p>İyi günler dileriz.</p>";


                CreateAdminMailDto mailData = new CreateAdminMailDto
                {
                    ReceiverMail = bookingViewModel.CreateBooking.Mail,
                    Subject = $"{bookingViewModel.CreateBooking.Date} Tarihli Rezervasyonunuz Hakkında",
                    Body = mailBody

                };

                await _sendMailServicePS.SendMailAsync(mailData);
                return RedirectToAction("Index");
            }

            bookingViewModel.HttpResponseMessage = response.StatusMessage;
            bookingViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(bookingViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateBooking(int id)
        {
            ViewBag.MainTitle = "Rezervasyonlar";

            var response = await _bookingServicePS.GetDataAsync(id);

            var model = new BookingViewModel();

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                var value = _mapper.Map<UpdateAdminBookingDto>(response.GetData);

                model.UpdateBooking = value;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateBooking(BookingViewModel bookingViewModel)
        {
            bookingViewModel.UpdateBooking.Status = true;

            var response = await _bookingServicePS.UpdateDataAsync(bookingViewModel.UpdateBooking);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                // Rezervasyon güncellendiğinde güncellenmiş rezervasyon bilgileri müşteriye mail olarak gönderilir.
                string bookingDetail = $@"
                <ul>
                    <li><strong>Tarih:</strong> {bookingViewModel.UpdateBooking.Date}</li>
                    <li><strong>Ad / Soyad:</strong> {bookingViewModel.UpdateBooking.Name}</li>
                    <li><strong>Kişi Sayısı:</strong> {bookingViewModel.UpdateBooking.PersonCount}</li>
                    <li><strong>Telefon:</strong> {bookingViewModel.UpdateBooking.Phone}</li>
                </ul>";


                string mailBody = $@"<p>Merhaba {bookingViewModel.UpdateBooking.Name}</p>
                    <p>Rezervasyonunuz <strong>{bookingViewModel.UpdateBooking.Date}</strong> tarihi olarak güncellenmiştir.</p>
                    <p>Rezervasyon bilgilerinize aşağıdan ulaşabilirsiniz.</p>
                    {bookingDetail}
                    <p>Siz değerli müşterimizi en lezzetli yemeklerimizle ağırlamak için sabırsızlanıyoruz.</p>
                    </p>İyi günler dileriz.</p>";


                CreateAdminMailDto mailData = new CreateAdminMailDto
                {
                    ReceiverMail = bookingViewModel.UpdateBooking.Mail,
                    Subject = $"{bookingViewModel.UpdateBooking.Date} Tarihli Rezervasyonunuz Hakkında",
                    Body = mailBody

                };

                await _sendMailServicePS.SendMailAsync(mailData);
                return RedirectToAction("Index");
            }

            bookingViewModel.HttpResponseMessage = response.StatusMessage;

            return View(bookingViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingServicePS.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
