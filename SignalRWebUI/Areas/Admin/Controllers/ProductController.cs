using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;
using SignalRWebUI.Areas.Admin.Dtos.ProductDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using SignalRWebUI.Utilities.FileOperations;
using System.Drawing.Printing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : AdminBaseController
    {
        private readonly IProductServicePS _productService;
        private readonly ICategoryServicePS _categoryService;
        private readonly IMapper _mapper;
        private readonly IFileOperation _fileOperation;

        public ProductController(IProductServicePS productService, ICategoryServicePS categoryService, IMapper mapper, IFileOperation fileOperation)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
            _fileOperation = fileOperation;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Ürünler";
            ViewBag.EmptyDataInfo = "Ürün";
            ViewBag.PageSize = 5; // Sayfada Gösterilecek Veri Sayısı - Dropdown ile kullanıcı seçimli yapılabilir.

            var responseMessage = await _productService.GetProductCount(); // Toplam ürün Sayısını Alır.
            var responseMessage2 = await _productService.GetAllDataAsync($"product/GetProductWithCategoriesForPaging?pagenumber=1&pagesize={ViewBag.PageSize}"); // Sayfalamaya Göre Ürün Getirir.

            var model = new ProductViewModel();

            if (responseMessage.StatusMessage == HttpStatusCode.OK)
            {
                if (responseMessage2.StatusMessage != HttpStatusCode.OK)
                {
                    model.HttpResponseMessage = responseMessage2.StatusMessage;
                }
                else
                {
                    int values = responseMessage.GetData;
                    var values2 = responseMessage2.GetData;
                    ViewBag.DataCount = values;

                    model.ResultProducts = _mapper.Map<List<ResultAdminProductDto>>(values2);
                }
            }
            else
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }

            return View(model);
        }

        [HttpGet]
        [Route("ProductWithCategoriesByPaging/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetProductWithCategoriesByPaging(int pageNumber, int pageSize)
        {
            ViewBag.MainTitle = "Ürünler";
            ViewBag.EmptyDataInfo = "Ürün";

            ViewBag.PageSize = 2;

            var responseMessage2 = await _productService.GetAllDataAsync($"product/GetProductWithCategoriesForPaging?pagenumber={pageNumber}&pagesize={pageSize}");

            var model = new ProductViewModel();

            if (responseMessage2.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage2.StatusMessage;
            }
            else
            {
                model.ResultProducts = _mapper.Map<List<ResultAdminProductDto>>(responseMessage2.GetData);
            }

            return Ok(model);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.MainTitle = "Ürünler";

            var model = new ProductViewModel();

            // Kategoriler dropdown için veritabanından getirilir.
            bool getCategoryStatus = await GetActiveCategories();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
        {

            string imageUrl = await SaveImageFileAsync(productViewModel.CreateProduct.Image);
            productViewModel.CreateProduct.ImageUrl = imageUrl;

            var response = await _productService.CreateDataAsync(productViewModel.CreateProduct);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Ürün Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            productViewModel.HttpResponseMessage = response.StatusMessage;
            productViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(productViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.MainTitle = "Ürünler";

            // Kategoriler dropdown için veritabanından getirilir.
            bool getCategoryStatus = await GetActiveCategories();

            var responseMessage = await _productService.GetDataAsync(id);

            var model = new ProductViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;

                ViewBag.ResponseMessage = $"Ürün Listelenirken Bir Hata Oluştu! StatusCode: {responseMessage.StatusCode}";
            }
            else
            {
                model.UpdateProduct = _mapper.Map<UpdateAdminProductDto>(responseMessage.GetData);
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id})")]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productViewModel)
        {

            if (productViewModel.UpdateProduct.Image != null)
            {
                string imageUrl = await SaveImageFileAsync(productViewModel.UpdateProduct.Image);
                productViewModel.UpdateProduct.ImageUrl = imageUrl;
            }

            productViewModel.UpdateProduct.CategoryID = Convert.ToInt32(productViewModel.UpdateProduct.CategoryID);

            var response = await _productService.UpdateDataAsync(productViewModel.UpdateProduct);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                productViewModel.HttpResponseMessage = response.StatusMessage;
                productViewModel.ApiResponseMessage = response.ApiResponseMessage;
            }

            ViewBag.ResponseMessage = $"Ürün Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            return View(productViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            await _productService.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }

        private async Task<bool> GetActiveCategories()
        {
            var response = await _categoryService.GetAllDataAsync(null);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                var values = _mapper.Map<List<ResultAdminCategoryDto>>(response.GetData);

                List<SelectListItem> categoryList = (from item in values
                                                     select new SelectListItem
                                                     {
                                                         Text = item.CategoryName,
                                                         Value = item.CategoryID.ToString()
                                                     }).ToList();

                categoryList.Insert(0, new SelectListItem
                {
                    Text = "Kategori Seçiniz",
                    Value = "",
                    Selected = true
                });

                ViewBag.CategoryList = categoryList;

                return true;
            }

            return false;
        }

        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {
            FileSettings fileSettings = new FileSettings
            {
                FolderPath = "\\wwwroot\\asset",
                FolderName = "defaultimage",
                UploadFile = imageFile
            };

            return await _fileOperation.SaveFileAsync(fileSettings);
        }
    }
}
