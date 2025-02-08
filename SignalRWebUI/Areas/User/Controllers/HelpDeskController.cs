using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos;
using SignalRWebUI.Areas.User.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/HelpDesk")]
    public class HelpDeskController : Controller
    {
        private readonly IHelpDeskServicePS _helpDeskService;

        public HelpDeskController(IHelpDeskServicePS helpDeskService)
        {
            _helpDeskService = helpDeskService;
        }

        public async Task<IActionResult> Index()
        {
            UserHelpDeskViewModel model = new UserHelpDeskViewModel();

            string email = User?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            var values = await _helpDeskService.GetDataUserOutboxByGuidAsync(email);

            model.HttpResponseMessage = values.StatusMessage;
            model.ApiResponseMessage = values.ApiResponseMessage;

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                model.HelpDeskListData = values.GetData;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateNewHelpDeskMessage()
        {
            UserHelpDeskViewModel model = new UserHelpDeskViewModel();

            model.CreateHelpDesk = new CreateAdminHelpDeskDto();

            model.CreateHelpDesk.NameSurname = User?.Claims.FirstOrDefault(c => c.Type == "namesurname")?.Value;
            model.CreateHelpDesk.Mail = User?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateNewHelpDeskMessage(UserHelpDeskViewModel helpDeskViewModel)
        {
            helpDeskViewModel.CreateHelpDesk.HelpDeskID = Guid.NewGuid();
            helpDeskViewModel.CreateHelpDesk.ReplyID = helpDeskViewModel.CreateHelpDesk.HelpDeskID;
            helpDeskViewModel.CreateHelpDesk.SendDate = DateTime.Now;

            var values = await _helpDeskService.CreateDataAsync(helpDeskViewModel.CreateHelpDesk);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            helpDeskViewModel.HttpResponseMessage = values.StatusMessage;
            helpDeskViewModel.ApiResponseMessage = values.ApiResponseMessage;

            return View(helpDeskViewModel);
        }

        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> HelpDeskMessageDetail(Guid id)
        {
            UserHelpDeskViewModel model = new UserHelpDeskViewModel();

            var values = await _helpDeskService.GetDataByGuidAsync(id);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                model.HelpDeskData = values.GetData;

                var values2 = await _helpDeskService.GetDataUserInboxByGuidAsync(model.HelpDeskData.Mail, model.HelpDeskData.ReplyID);

                if (values2.StatusMessage == HttpStatusCode.OK)
                {
                    model.HelpDeskListData = values2.GetData;
                }
                else
                {
                    model.HttpResponseMessage = values2.StatusMessage;
                    model.ApiResponseMessage = values2.ApiResponseMessage;
                }
            }
            else
            {
                model.HttpResponseMessage = values.StatusMessage;
                model.ApiResponseMessage = values.ApiResponseMessage;
            }

            return View(model);
        }
    }
}
