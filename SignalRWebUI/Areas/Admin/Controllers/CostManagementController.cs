using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;
using SignalRWebUI.Areas.Admin.Dtos.MoneyCaseDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CostManagement")]
    public class CostManagementController : AdminBaseController
    {
        private readonly IMoneyCaseServicePS _moneyCaseServicePS;
        private readonly IMapper _mapper;

        public CostManagementController(IMoneyCaseServicePS moneyCaseServicePS, IMapper mapper)
        {
            _moneyCaseServicePS = moneyCaseServicePS;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // Ayın ilk günü ve son günü arasındaki tüm gider kalemlerini listeler.

            ViewBag.MainTitle = "Aylık Gider Listesi";
            var responseMessage = await _moneyCaseServicePS.GetAllOutcomeDataAsync();

            CostManagementViewModel model = new CostManagementViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                model.ResultMoneyCases = _mapper.Map<List<ResultAdminMoneyCaseDto>>(responseMessage.GetData);
            }

            return View(model);
        }

        [HttpGet]
        [Route("History")]
        public async Task<IActionResult> History()
        {
            ViewBag.MainTitle = "Aylık Gelir - Gider Listesi";
            var responseMessage = await _moneyCaseServicePS.GetAllDataAsync(null);

            CostManagementViewModel model = new CostManagementViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                model.ResultMoneyCases = _mapper.Map<List<ResultAdminMoneyCaseDto>>(responseMessage.GetData);
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateNewCost()
        {
            // Yeni gider kalemi ekler.
            ViewBag.MainTitle = "Yeni Gider Ekleme";
            CostManagementViewModel model = new CostManagementViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateNewCost(CostManagementViewModel costManagementViewModel)
        {
            costManagementViewModel.CreateMoneyCase.OperationType = "outcome";
            var responseMessage = await _moneyCaseServicePS.CreateDataAsync(costManagementViewModel.CreateMoneyCase);

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Gider Eklenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";

            costManagementViewModel.HttpResponseMessage = responseMessage.StatusMessage;
            costManagementViewModel.ApiResponseMessage = responseMessage.ApiResponseMessage;

            return View(costManagementViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateCost(int id)
        {
            // Gider kalemini günceller.
            ViewBag.MainTitle = "Gider Düzenleme";
            var responseMessage = await _moneyCaseServicePS.GetDataAsync(id);

            CostManagementViewModel model = new CostManagementViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                model.UpdateMoneyCase = _mapper.Map<UpdateAdminMoneyCaseDto>(responseMessage.GetData);
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateCost(CostManagementViewModel costManagementViewModel)
        {
            var responseMessage = await _moneyCaseServicePS.UpdateDataAsync(costManagementViewModel.UpdateMoneyCase);

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Kategori Güncellenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";

            costManagementViewModel.HttpResponseMessage = responseMessage.StatusMessage;
            costManagementViewModel.ApiResponseMessage = responseMessage.ApiResponseMessage;

            return View(costManagementViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> CreateNewCost(int id)
        {
            await _moneyCaseServicePS.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
