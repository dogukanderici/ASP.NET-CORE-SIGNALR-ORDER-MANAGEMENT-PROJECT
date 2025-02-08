using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.ContactDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateContactDto> _createValidator;
        private readonly IValidator<UpdateContactDto> _updateValidator;

        public ContactController(IContactService contactService, IMapper mapper, IValidator<CreateContactDto> createValidator, IValidator<UpdateContactDto> updateValidator)
        {
            _contactService = contactService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult ContactList()
        {
            IDataResult<List<Contact>> values = _contactService.TGetAllData();
            List<ResultContactDto> valueToDto = _mapper.Map<List<ResultContactDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetContactById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetContactById(int id)
        {
            IDataResult<Contact> value = _contactService.TGetData(x => x.ContactID == id);
            ResultContactDto valueToDto = _mapper.Map<ResultContactDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult AddContact(CreateContactDto createContactDto)
        {
            var validatorResult = _createValidator.Validate(createContactDto);

            if(!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Contact valueFromDto = _mapper.Map<Contact>(createContactDto);
            IResult values = _contactService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "İletişim Bilgisi Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            var validatorResult = _updateValidator.Validate(updateContactDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Contact valueFromDto = _mapper.Map<Contact>(updateContactDto);
            IResult values = _contactService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "İletişim Bilgisi Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteContact(int id)
        {
            IDataResult<bool> state = _contactService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "İletişim Bilgisi Verisi Başarıyla Silindi." });
        }
    }
}
