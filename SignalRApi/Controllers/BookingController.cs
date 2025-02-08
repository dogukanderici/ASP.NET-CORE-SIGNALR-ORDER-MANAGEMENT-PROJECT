using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Dtos.Dtos.BookingDtos;
using SignalR.Entities.Concrete;
using System.Net;
using IResult = SignalR.Core.Utilities.Result.IResult;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly IRestaurantTableService _restaurantTableService;
        private readonly IValidator<CreateBookingDto> _createValidator;
        private readonly IValidator<UpdateBookingDto> _updateValidator;

        public BookingController(IBookingService bookingService, IMapper mapper, IRestaurantTableService restaurantTableService, IValidator<CreateBookingDto> createValidator, IValidator<UpdateBookingDto> updateValidator)
        {
            _bookingService = bookingService;
            _mapper = mapper;
            _restaurantTableService = restaurantTableService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult BookingList()
        {
            IDataResult<List<Booking>> values = _bookingService.TGetAllData();
            List<ResultBookingDto> valueToDto = _mapper.Map<List<ResultBookingDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetBookingListByUser")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult BookingListByUserMail(string userMail)
        {
            IDataResult<List<Booking>> values = _bookingService.TGetAllData(b => b.Mail == userMail);
            List<ResultBookingDto> valueToDto = _mapper.Map<List<ResultBookingDto>>(values.Data);

            return Ok(valueToDto);
        }

        [HttpGet("GetBookingById")]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult GetBookingById(int id)
        {
            IDataResult<Booking> value = _bookingService.TGetData(x => x.BookingID == id);
            ResultBookingDto valueToDto = _mapper.Map<ResultBookingDto>(value.Data);

            return Ok(valueToDto);
        }

        [HttpPost]
        [Authorize(Policy = "ReadPermissionPolicy")]
        public IActionResult AddBooking(CreateBookingDto createBookingDto)
        {
            createBookingDto.Status = false;
            createBookingDto.IsConfirmed = false;

            var validatorResult = _createValidator.Validate(createBookingDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            // Kişi sayısı 2 ve daha azsa 2 kişilik masalara, 2'den fazlaysa 4 kişilik masalara sorgu kriteri oluşturur.
            int personCountForQuery = (createBookingDto.PersonCount <= 2 ? 2 : 4);

            // Masalardan rezervasyon yapılacak kişi sayısına uygun masalar çekilir.
            IDataResult<List<RestaurantTable>> valuesAllDataForTable = _restaurantTableService.TGetAllData(rt => rt.PersonCount == personCountForQuery);

            DateTime controlStartDate = createBookingDto.Date.Date.AddHours(0).AddMinutes(0);
            DateTime controlEndDate = createBookingDto.Date.Date.AddHours(23).AddMinutes(59);
            IDataResult<List<Booking>> valuesAllDataForBooking = _bookingService.TGetAllData(
                b => b.Date <= controlEndDate && b.Date >= controlStartDate &&
                b.PersonCount == personCountForQuery);
            List<ResultBookingDto> valueToDto = _mapper.Map<List<ResultBookingDto>>(valuesAllDataForBooking.Data);

            if (valuesAllDataForTable.Data.Count() <= valuesAllDataForBooking.Data.Count())
            {
                return BadRequest("Rezervasyon Yapılacak Boş Masa Bulunamadı!");
            }

            Booking valueFromDto = _mapper.Map<Booking>(createBookingDto);
            IResult values = _bookingService.TAddData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Rezervasyon Verisi Başarıyla Eklendi." });
        }

        [HttpPut]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var validatorResult = _updateValidator.Validate(updateBookingDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            Booking valueFromDto = _mapper.Map<Booking>(updateBookingDto);
            IResult values = _bookingService.TUpdateData(valueFromDto);

            if (values.SuccessCode != HttpStatusCode.OK)
            {
                return StatusCode((int)values.SuccessCode, values);
            }

            return Ok(new { success = true, message = "Rezervasyon Bilgisi Başarıyla Güncellendi." });
        }

        [HttpDelete]
        [Authorize(Policy = "WritePermissionPolicy")]
        public IActionResult DeleteBooking(int id)
        {
            IDataResult<bool> state = _bookingService.TDeleteData(id);

            if (!state.SuccessStatus)
            {
                return BadRequest(new { success = false, message = "Silinecek Veri Bulunamadı" });
            }

            return Ok(new { success = true, message = "Rezervasyon Verisi Başarıyla Silindi." });
        }
    }
}
