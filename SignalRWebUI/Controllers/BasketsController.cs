using Microsoft.AspNetCore.Mvc;
using SignalR.Dtos.Dtos.Discounts;
using SignalRWebUI.Areas.Admin.Dtos.BasketDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Controllers
{
    [Route("Baskets")]
    public class BasketsController : Controller
    {
        private readonly IBasketServicePS _basketServicePS;
        private readonly IDiscountServicePS _discountServicePS;

        public BasketsController(IBasketServicePS basketServicePS, IDiscountServicePS discountServicePS)
        {
            _basketServicePS = basketServicePS;
            _discountServicePS = discountServicePS;
        }

        public async Task<IActionResult> Index()
        {
            var tableID = HttpContext.Request.Cookies["MasaID"];

            var response = await _basketServicePS.GetAllDataAsync(tableID);

            BasketViewModel model = new BasketViewModel();
            model.DiscountData = new ResultDiscountDto();
            model.DiscountData.Description = null;
            model.DiscountData.Title = null;
            model.DiscountData.Amount = 0;
            model.DiscountData.LastDay = DateTime.Now.Date.AddHours(23).AddMinutes(59);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                model.BasketListData = response.GetData;

                decimal totalBasketPrice = response.GetData.Select(x => x.TotalPrice).Sum();
                model.TotalBasketPrice = totalBasketPrice;
                model.TotalBasketPriceWithDiscount = model.TotalBasketPrice - ((model.TotalBasketPrice * model.DiscountData.Amount) / 100);
            }
            else
            {
                model.HttpResponseMessage = response.StatusMessage;
                model.ApiResponseMessage = response.ApiResponseMessage;
            }

            return View(model);
        }

        [HttpGet]
        [Route("CodeIndex")]
        public async Task<IActionResult> Index(string code)
        {
            var masaID = HttpContext.Request.Cookies["MasaID"];
            var responseBasket = await _basketServicePS.GetAllDataAsync(masaID);
            var responseDiscount = await _discountServicePS.GetDataByTitleAsync(code);

            BasketViewModel model = new BasketViewModel();

            if (responseBasket.StatusMessage == HttpStatusCode.OK)
            {
                if (responseDiscount.StatusMessage == HttpStatusCode.OK)
                {
                    if (responseDiscount.GetData.LastDay >= DateTime.Now)
                    {
                        model.DiscountData = responseDiscount.GetData;
                    }
                    else
                    {
                        model.HttpResponseMessage = HttpStatusCode.BadRequest;
                        model.ApiResponseMessage = "İndirim Kuponunun Süresi Dolmuş!";

                        model.DiscountData = new ResultDiscountDto();
                        model.DiscountData.Description = null;
                        model.DiscountData.Title = null;
                        model.DiscountData.Amount = 0;
                        model.DiscountData.LastDay = DateTime.Now.Date.AddHours(23).AddMinutes(59);
                    }
                }
                else
                {
                    model.HttpResponseMessage = responseDiscount.StatusMessage;
                    model.ApiResponseMessage = responseDiscount.StatusMessage == HttpStatusCode.NoContent ? "Kupon Bulunamadı!" : responseDiscount.ApiResponseMessage;

                    model.DiscountData = new ResultDiscountDto();
                }

                model.BasketListData = responseBasket.GetData;

                decimal totalBasketPrice = responseBasket.GetData.Select(x => x.TotalPrice).Sum();
                model.TotalBasketPrice = totalBasketPrice;
                model.TotalBasketPriceWithDiscount = model.TotalBasketPrice - ((model.TotalBasketPrice * model.DiscountData.Amount) / 100);
            }
            else
            {
                model.HttpResponseMessage = responseDiscount.StatusMessage;
                model.ApiResponseMessage = responseDiscount.ApiResponseMessage;
            }

            return View(model);
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItemBasket([FromBody] CreateAdminBasketDto createAdminBasketDto)
        {
            BasketViewModel basketViewModel = new BasketViewModel();

            var response = await _basketServicePS.CreateDataAsync(createAdminBasketDto);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                basketViewModel.HttpResponseMessage = response.StatusMessage;
                basketViewModel.ApiResponseMessage = response.ApiResponseMessage;
            }

            return (response.StatusMessage == HttpStatusCode.OK ? Ok(new { status = true, message = "İşlem Başarılı" }) : BadRequest(new { status = false, message = "İşlem Başarısız" }));
        }

        [HttpDelete]
        [Route("RemoveItem")]
        public async Task<IActionResult> RemoveItemFormBasket(int id, int tableID)
        {
            await _basketServicePS.DeleteBasketItemAsync(id, tableID);

            return RedirectToAction("MainIndex", "Baskets");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAllBasket(int id)
        {
            await _basketServicePS.DeleteDataAsync(id);

            return RedirectToAction("MainIndex", "Baskets");
        }
    }
}
