using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/HelpDesk")]
    public class HelpDeskController : Controller
    {
        private readonly IHelpDeskServicePS _helpDeskServicePS;
        private readonly IMapper _mapper;

        public HelpDeskController(IHelpDeskServicePS helpDeskServicePS, IMapper mapper)
        {
            _helpDeskServicePS = helpDeskServicePS;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Help Desk Mesajları";

            HelpDeskViewModel model = new HelpDeskViewModel();

            var values = await _helpDeskServicePS.GetAllDataAsync(null);

            if (values.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = values.StatusMessage;
            }
            else
            {
                model.HelpDeskList = _mapper.Map<List<ResultAdminHelpDeskDto>>(values.GetData);
            }

            return View(model);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateHelpDesk(Guid id)
        {
            ViewBag.MainTitle = "Help Desk Mesajları";

            HelpDeskViewModel model = new HelpDeskViewModel();

            var values = await _helpDeskServicePS.GetDataByGuidAsync(id);
            var values2 = await _helpDeskServicePS.GetDataAdminOutboxByGuidAsync(id);

            if (values.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = values.StatusMessage;
            }
            else if (values2.StatusMessage != HttpStatusCode.OK)
            {

                model.HttpResponseMessage = values2.StatusMessage;
            }
            else
            {
                model.UpdateHelpDesk = _mapper.Map<UpdateAdminHelpDeskDto>(values.GetData);
                model.HelpDeskList = _mapper.Map<List<ResultAdminHelpDeskDto>>(values2.GetData);
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateHelpDesk(HelpDeskViewModel helpDeskViewModel)
        {
            var values = await _helpDeskServicePS.UpdateDataAsync(helpDeskViewModel.UpdateHelpDesk);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            helpDeskViewModel.HttpResponseMessage = values.StatusMessage;
            helpDeskViewModel.ApiResponseMessage = values.ApiResponseMessage;

            return View(helpDeskViewModel);
        }

        [HttpGet]
        [Route("Reply/{id}")]
        public async Task<IActionResult> ReplyHelpDeskMessage(Guid id)
        {
            HelpDeskViewModel model = new HelpDeskViewModel();

            var values = await _helpDeskServicePS.GetDataByGuidAsync(id);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                model.UpdateHelpDesk = _mapper.Map<UpdateAdminHelpDeskDto>(values.GetData);
            }
            else
            {
                model.HttpResponseMessage = values.StatusMessage;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Reply/{id}")]
        public async Task<IActionResult> ReplyHelpDeskMessage(HelpDeskViewModel helpDeskViewModel)
        {
            helpDeskViewModel.ReplyHelpDesk.NameSurname = "Admin";
            helpDeskViewModel.ReplyHelpDesk.Mail = "admin@testmail.com";
            helpDeskViewModel.ReplyHelpDesk.Phone = "0555 555 55 55";
            helpDeskViewModel.ReplyHelpDesk.Subject = helpDeskViewModel.UpdateHelpDesk.Subject;
            helpDeskViewModel.ReplyHelpDesk.SendDate = DateTime.Now;
            helpDeskViewModel.ReplyHelpDesk.ReplyID = helpDeskViewModel.UpdateHelpDesk.ReplyID;

            var response = await _helpDeskServicePS.CreateDataAsync(helpDeskViewModel.ReplyHelpDesk);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            helpDeskViewModel.HttpResponseMessage = response.StatusMessage;
            helpDeskViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(helpDeskViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteHelpDesk(Guid id)
        {
            await _helpDeskServicePS.DeleteDataByGuidAsync(id);

            return RedirectToAction("Index");
        }
    }
}
