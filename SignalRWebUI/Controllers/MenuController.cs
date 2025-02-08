using Microsoft.AspNetCore.Mvc;
using SignalR.Dtos.Dtos.ProductDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.Controllers
{
    [Route("Menu")]
    public class MenuController : Controller
    {
        private readonly IProductServicePS _productServicePS;
        private readonly ICategoryServicePS _categoryServicePS;

        public MenuController(IProductServicePS productServicePS, ICategoryServicePS categoryServicePS)
        {
            _productServicePS = productServicePS;
            _categoryServicePS = categoryServicePS;
        }

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.MasaID = HttpContext.Request.Cookies["MasaID"];

            MenuViewModel model = new MenuViewModel();

            var responseProducts = await _productServicePS.GetAllDataAsync(null);
            var responseCategory = await _categoryServicePS.GetAllDataAsync(null);

            if (responseCategory.StatusMessage == HttpStatusCode.OK)
            {
                if (responseProducts.StatusMessage == HttpStatusCode.OK)
                {
                    model.ProductList = responseProducts.GetData;
                    model.CategoryList = responseCategory.GetData;
                }
                else
                {
                    model.HttpResponseMessage = responseProducts.StatusMessage;
                    model.ApiResponseMessage = responseProducts.ApiResponseMessage;
                }
            }
            else
            {
                model.HttpResponseMessage = responseCategory.StatusMessage;
                model.ApiResponseMessage = responseCategory.ApiResponseMessage;
            }

            return View(model);
        }

        [HttpGet]
        [Route("ProductsByCategory/{id}")]
        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            var response = (id == 0 ? await _productServicePS.GetAllDataByCountAsync(9) : await _productServicePS.GetAllDataByCategoryAsync(id));

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return Ok(response.GetData);
            }
            else
            {
                return BadRequest(new { status = response.StatusMessage, message = response.ApiResponseMessage });
            }
        }
    }
}
