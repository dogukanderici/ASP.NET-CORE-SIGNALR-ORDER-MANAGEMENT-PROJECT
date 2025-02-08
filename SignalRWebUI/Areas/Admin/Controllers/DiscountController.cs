using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Areas.Admin.Dtos.DiscountDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Discount")]
    public class DiscountController : AdminBaseController
    {
        private readonly IDiscountServicePS _discountServicePS;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountServicePS discountServicePS, IMapper mapper)
        {
            _discountServicePS = discountServicePS;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            DiscountViewModel model = new DiscountViewModel();
            ViewBag.MainTitle = "İndirim Listesi";

            var values = await _discountServicePS.GetAllDataAsync(null);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                model.DiscountDataList = values.GetData;
            }
            else
            {
                model.HttpResponseMessage = values.StatusMessage;
                model.ApiResponseMessage = values.ApiResponseMessage;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateDiscount()
        {
            DiscountViewModel model = new DiscountViewModel();
            ViewBag.MainTitle = "Yeni İndirim Ekle";

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateDiscount(DiscountViewModel discountViewModel)
        {
            var valuesForCheck = await _discountServicePS.GetDataByTitleAsync(discountViewModel.CreateDiscountData.Title);

            if (valuesForCheck.StatusMessage == HttpStatusCode.OK || valuesForCheck.StatusMessage == HttpStatusCode.NoContent)
            {
                // Aynı Title İle Başka bir İndirim Olup Olmadığını Kontrol Eder.
                if (valuesForCheck.GetData != null)
                {
                    discountViewModel.HttpResponseMessage = HttpStatusCode.BadRequest;
                    discountViewModel.ApiResponseMessage = "Bu Başlık İle İndirim Bulunmaktadır!";

                    return View(discountViewModel);
                }
                else
                {
                    discountViewModel.CreateDiscountData.DiscountID = Guid.NewGuid();
                    var values = await _discountServicePS.CreateDataAsync(discountViewModel.CreateDiscountData);

                    if (values.StatusMessage == HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        discountViewModel.HttpResponseMessage = values.StatusMessage;
                        //discountViewModel.ApiResponseMessage = values.ApiResponseMessage;
                        ModelState.AddModelError(string.Empty, values.ApiResponseMessage);
                    }
                }
            }
            else
            {
                discountViewModel.HttpResponseMessage = valuesForCheck.StatusMessage;
                discountViewModel.ApiResponseMessage = valuesForCheck.ApiResponseMessage;
            }

            return View(discountViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateDiscount(Guid id)
        {
            DiscountViewModel model = new DiscountViewModel();
            ViewBag.MainTitle = "İndirim Güncelleme";

            var values = await _discountServicePS.GetDataByGuidAsync(id);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                model.UpdateDiscountData = _mapper.Map<UpdateAdminDiscountDto>(values.GetData);
            }
            else
            {
                model.HttpResponseMessage = values.StatusMessage;
                model.ApiResponseMessage = values.ApiResponseMessage;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateDiscount(DiscountViewModel discountViewModel)
        {
            var values = await _discountServicePS.UpdateDataAsync(discountViewModel.UpdateDiscountData);

            if (values.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            discountViewModel.HttpResponseMessage = values.StatusMessage;
            discountViewModel.ApiResponseMessage = values.ApiResponseMessage;

            return View(discountViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> CreateDiscount(Guid id)
        {
            await _discountServicePS.DeleteDataByGuidAsync(id);

            return RedirectToAction("Index");
        }
    }
}
