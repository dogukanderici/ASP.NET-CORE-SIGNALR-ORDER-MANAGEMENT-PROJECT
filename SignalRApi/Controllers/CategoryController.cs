using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Business.Utilities.Validations.CategoryValidations;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.CategoryDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;

        public CategoryController(ICategoryService categoryService, IMapper mapper, IValidator<CreateCategoryDto> createValidator, IValidator<UpdateCategoryDto> updateValidator)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult CategoryList()
        {
            IDataResult<List<Category>> values = _categoryService.TGetAllData();
            List<ResultCategoryDto> valueToDto = _mapper.Map<List<ResultCategoryDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetCategoryById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetCategoryById(int id)
        {
            IDataResult<Category> value = _categoryService.TGetData(x => x.CategoryID == id);
            ResultCategoryDto valueToDto = _mapper.Map<ResultCategoryDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpGet]
        [Route("GetActiveCategoryCount")]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult GetActiveCategoryCount()
        {
            IDataResult<int> value = _categoryService.GetCategoryActiveCount();

            return Ok(value.Data);
        }

        [HttpPost]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult AddCategory(CreateCategoryDto createCategoryDto)
        {
            var validatorResult = _createValidator.Validate(createCategoryDto);

            if(!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Category valueFromDto = _mapper.Map<Category>(createCategoryDto);
            IResult values = _categoryService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Kategori Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var validatorResult = _updateValidator.Validate(updateCategoryDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Category valueFromDto = _mapper.Map<Category>(updateCategoryDto);
            IResult values = _categoryService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Kategori Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteCategory(int id)
        {
            IDataResult<bool> state = _categoryService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Kategori Verisi Başarıyla Silindi." });
        }
    }
}
