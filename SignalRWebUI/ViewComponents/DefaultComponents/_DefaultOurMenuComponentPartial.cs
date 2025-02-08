using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using System.Net;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultOurMenuComponentPartial : ViewComponent
    {
        private readonly ICategoryServicePS _categoryServicePS;
        private readonly IProductServicePS _productServicePS;

        public _DefaultOurMenuComponentPartial(ICategoryServicePS categoryServicePS, IProductServicePS productServicePS)
        {
            _categoryServicePS = categoryServicePS;
            _productServicePS = productServicePS;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            MenuViewModel model = new MenuViewModel();

            var responseProducts = await _productServicePS.GetAllDataByCountAsync(9);
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
    }
}
