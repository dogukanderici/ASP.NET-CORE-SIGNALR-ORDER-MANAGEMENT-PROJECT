using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Areas.Admin.Dtos.SendMailDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Mail")]
    public class MailController : Controller
    {
        private readonly ISendMailServicePS _sendMailServicePS;

        public MailController(ISendMailServicePS sendMailServicePS)
        {
            _sendMailServicePS = sendMailServicePS;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.MainTitle = "Mail Gönderimi";

            SendMailViewModel model = new SendMailViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SendMailViewModel sendMailViewModel)
        {
            var response = await _sendMailServicePS.SendMailAsync(sendMailViewModel.CreateNewMail);

            if (response.StatusMessage == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            sendMailViewModel.HttpResponseMessage = response.StatusMessage;

            return View(sendMailViewModel);
        }
    }
}
