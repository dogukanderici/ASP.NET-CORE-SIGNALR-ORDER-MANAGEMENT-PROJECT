using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.WorkingHourDtos;
using SignalRWebUI.Areas.Admin.Dtos.WorkingHourDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using SignalRWebUI.Utilities.FileOperations;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/WorkingHour")]
    public class WorkingHourController : AdminBaseController
    {
        private readonly IWorkingHourServicePS _workingHourService;
        private readonly IMapper _mapper;

        public WorkingHourController(IWorkingHourServicePS workingHourService, IMapper mapper)
        {
            _workingHourService = workingHourService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Çalışma Saatleri";
            ViewBag.EmptyDataInfo = "Çalışma Saati";

            var responseMessage = await _workingHourService.GetAllDataAsync(null);

            WorkingHourViewModel model = new WorkingHourViewModel();

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                List<ResultWorkingHourDto> values = responseMessage.GetData;
                model.ResultWorkingHours = values;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> CreateWorkingHour()
        {
            ViewBag.MainTitle = "Çalışma Saatleri";

            WorkingHourViewModel model = new WorkingHourViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateWorkingHour(WorkingHourViewModel workinghourViewModel)
        {
            var response = await _workingHourService.CreateDataAsync(workinghourViewModel.CreateWorkingHour);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Çalışma Saati Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            workinghourViewModel.HttpResponseMessage = response.StatusMessage;
            workinghourViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(workinghourViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateWorkingHour(int id)
        {
            ViewBag.MainTitle = "Çalışma Saatleri";

            var responseMessage = await _workingHourService.GetDataAsync(id);

            WorkingHourViewModel model = new WorkingHourViewModel();

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                UpdateAdminWorkingHourDto values = _mapper.Map<UpdateAdminWorkingHourDto>(responseMessage.GetData);
                model.UpdateWorkingHour = values;
            }
            else
            {
                ViewBag.ResponseMessage = $"Çalışma Saati Listelenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";

                model.HttpResponseMessage = responseMessage.StatusMessage;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}\")")]
        public async Task<IActionResult> UpdateWorkingHour(WorkingHourViewModel workinghourViewModel)
        {
            var response = await _workingHourService.UpdateDataAsync(workinghourViewModel.UpdateWorkingHour);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ResponseMessage = $"Çalışma Saati Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

                workinghourViewModel.HttpResponseMessage = response.StatusMessage;
            }

            return View(workinghourViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteWorkingHour(int id)
        {
            await _workingHourService.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
