using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;

namespace SignalRWebUI.Controllers
{
    public class SignalRDefaultController : Controller
    {
        private readonly ICategoryService _categoryService;

        public SignalRDefaultController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var value = _categoryService.GetCategoryActiveCount();

            return View();
        }
        public IActionResult Index2()
        {
            var value = _categoryService.GetCategoryActiveCount();

            return View();
        }
    }
}
