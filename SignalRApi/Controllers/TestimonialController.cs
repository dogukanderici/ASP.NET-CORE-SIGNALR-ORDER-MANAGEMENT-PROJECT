using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.TestimonialDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTestimonialDto> _createValidator;
        private readonly IValidator<UpdateTestimonialDto> _updateValidator;

        public TestimonialController(ITestimonialService testimonialService, IMapper mapper, IValidator<CreateTestimonialDto> createValidator, IValidator<UpdateTestimonialDto> updateValidator)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult TestimonialList()
        {
            IDataResult<List<Testimonial>> values = _testimonialService.TGetAllData();
            List<ResultTestimonialDto> valueToDto = _mapper.Map<List<ResultTestimonialDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetTestimonialById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetTestimonialById(int id)
        {
            IDataResult<Testimonial> value = _testimonialService.TGetData(x => x.TestimonialID == id);
            ResultTestimonialDto valueToDto = _mapper.Map<ResultTestimonialDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult AddTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var validatorResult = _createValidator.Validate(createTestimonialDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Testimonial valueFromDto = _mapper.Map<Testimonial>(createTestimonialDto);
            IResult values = _testimonialService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Referans Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            var validatorResult = _updateValidator.Validate(updateTestimonialDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Testimonial valueFromDto = _mapper.Map<Testimonial>(updateTestimonialDto);
            IResult values = _testimonialService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Referans Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteTestimonial(int id)
        {
            IDataResult<bool> state = _testimonialService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Referans Verisi Başarıyla Silindi." });
        }
    }
}
