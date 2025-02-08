using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Business.Settings;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.ProductDtos;
using SignalR.Dtos.Dtos.PropertySettingsDtos;
using SignalR.Entities.Concrete;
using SignalRApi.Models;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;

        public ProductController(IProductService productService, IMapper mapper, IValidator<CreateProductDto> createValidator, IValidator<UpdateProductDto> updateValidator)
        {
            _productService = productService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult ProductList()
        {
            IDataResult<List<Product>> values = _productService.TGetAllData(null, p => p.Category);
            List<ResultProductDto> valueToDto = _mapper.Map<List<ResultProductDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetProductById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetProductById(int id)
        {
            IDataResult<Product> value = _productService.TGetData(x => x.ProductID == id, p => p.Category);
            ResultProductDto valueToDto = _mapper.Map<ResultProductDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetProductsWithCategories")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetProductsWithCategories(int id)
        {
            IDataResult<List<Product>> values = _productService.TGetProductsWithCategories(p => p.Category, p => p.CategoryID == id);
            List<ResultProductDto> valueToDto = _mapper.Map<List<ResultProductDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetProductsWithCategoriesByCount")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetProductsWithCategoriesByCount(int dataCount)
        {
            // İçecek Kategorisinde Olmayan ve Kategorisi Aktif Olan Ürünler Listelenir.
            IDataResult<List<Product>> values = _productService.TGetProductsWithCategoriesByCount(dataCount, p => p.Category, p => p.CategoryID != 8 && p.Category.CategoryStatus == true);
            List<ResultProductDto> valueToDto = _mapper.Map<List<ResultProductDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet]
        [Route("GetProductCount")]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetProductCount()
        {
            IDataResult<int> value = _productService.TGetProductCount();

            return Ok(value.Data);
        }

        [HttpGet]
        [Route("GetProductCountWithDrink")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetProductCountWithDrink()
        {
            IDataResult<int> value = _productService.TGetProductCountByCategoryNameDrink();

            return Ok(value.Data);
        }

        [HttpGet]
        [Route("GetAverageProductPrice")]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetAverageProductPrice()
        {
            IDataResult<decimal> value = _productService.TGetAverageProductPrice();

            return Ok(value.Data);
        }

        [HttpGet]
        [Route("GetMaxProductPrice")]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetMaxProductPrice()
        {
            IDataResult<PropertySettings> value = _productService.TGetMaxProductWithPrice();

            return Ok(_mapper.Map<PropertySettingsDto>(value.Data));
        }

        [HttpGet]
        [Route("GetMinProductPrice")]
        [Authorize(Policy = "AdminPermissionPolicy")]
        public IActionResult GetMinProductPrice()
        {
            IDataResult<PropertySettings> value = _productService.TGetMinProductWithPrice();

            return Ok(value.Data);
        }

        [HttpGet]
        [Route("GetProductWithCategoriesForPaging")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetProductWithCategoriesForPaging(int pageNumber, int pageSize)
        {
            IDataResult<int> dataCount = _productService.TGetProductCount();
            IDataResult<List<Product>> values = _productService.TGetProductsWithCategoriesForPaging(pageNumber, pageSize, p => p.Category, null);

            PagingDataViewModel<Product> model = new PagingDataViewModel<Product>();

            model.Data = values.Data;
            model.Count = dataCount.Data;

            return Ok(model);
        }

        [HttpPost]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult AddProduct(CreateProductDto createProductDto)
        {
            var validatorResult = _createValidator.Validate(createProductDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Product valueFromDto = _mapper.Map<Product>(createProductDto);
            IResult values = _productService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Ürün Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var validatorResult = _updateValidator.Validate(updateProductDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Product valueFromDto = _mapper.Map<Product>(updateProductDto);
            IResult values = _productService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Ürün Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteProduct(int id)
        {
            IDataResult<bool> state = _productService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Ürün Verisi Başarıyla Silindi." });
        }
    }
}
