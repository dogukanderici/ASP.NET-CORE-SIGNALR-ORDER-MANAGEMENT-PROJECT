using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : AdminBaseController
    {

        private readonly ICategoryServicePS _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryServicePS categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Kategoriler";

            var responseMessage = await _categoryService.GetAllDataAsync(null);

            var model = new CategoryViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                model.ResultCategories = _mapper.Map<List<ResultAdminCategoryDto>>(responseMessage.GetData);
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult AddCategory()
        {
            ViewBag.MainTitle = "Kategoriler";

            var model = new CategoryViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddCategory(CategoryViewModel categoryViewModel)
        {

            var responseMessage = await _categoryService.CreateDataAsync(categoryViewModel.CreateCategory);

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Kategori Eklenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";

            categoryViewModel.HttpResponseMessage = responseMessage.StatusMessage;
            categoryViewModel.ApiResponseMessage = responseMessage.ApiResponseMessage;

            return View(categoryViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            ViewBag.MainTitle = "Kategoriler";

            var responseMessage = await _categoryService.GetDataAsync(id);

            var model = new CategoryViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                model.UpdateCategory = _mapper.Map<UpdateAdminCategoryDto>(responseMessage.GetData);
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel categoryViewModel)
        {

            var responseMessage = await _categoryService.UpdateDataAsync(categoryViewModel.UpdateCategory);

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Kategori Güncellenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";

            categoryViewModel.HttpResponseMessage = responseMessage.StatusMessage;
            categoryViewModel.ApiResponseMessage = responseMessage.ApiResponseMessage;

            return View(categoryViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
