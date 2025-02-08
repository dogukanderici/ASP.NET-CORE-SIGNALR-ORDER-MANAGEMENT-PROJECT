using AutoMapper;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class BookingServicePS : IBookingServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public BookingServicePS(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultBookingDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultBookingDto>> responseMessage = new HttpResponseMessageSettings<List<ResultBookingDto>>();

            var response = await _httpClient.GetAsync("booking");

            List<ResultBookingDto> values = new List<ResultBookingDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultBookingDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultBookingDto>>> GetAllDataByUserAsync(string userMail)
        {
            HttpResponseMessageSettings<List<ResultBookingDto>> responseMessage = new HttpResponseMessageSettings<List<ResultBookingDto>>();
            List<ResultBookingDto> values = new List<ResultBookingDto>();

            var response = await _httpClient.GetAsync($"booking/getbookinglistbyuser?usermail={userMail}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultBookingDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultBookingDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultBookingDto> responseMessage = new HttpResponseMessageSettings<ResultBookingDto>();

            var response = await _httpClient.GetAsync("booking/getbookingbyid?id=" + id);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultBookingDto values = await response.Content.ReadFromJsonAsync<ResultBookingDto>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminBookingDto>> CreateDataAsync(CreateAdminBookingDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminBookingDto> responseMessage = new HttpResponseMessageSettings<CreateAdminBookingDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminBookingDto>("booking", createMdoel);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            var apiResponse = JsonConvert.DeserializeObject<ApiValidationResponseConstant>(await response.Content.ReadAsStringAsync());
            if (apiResponse?.Errors != null)
            {
                foreach (var errorItems in apiResponse.Errors)
                {
                    foreach (var errorItem in errorItems.Value)
                    {
                        responseMessage.ApiResponseMessage += errorItem + ',';
                    }
                }
            }
            else
            {
                if (!response.IsSuccessStatusCode)
                {

                    responseMessage.ApiResponseMessage = await response.Content.ReadAsStringAsync();
                }
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<UpdateAdminBookingDto>> UpdateDataAsync(UpdateAdminBookingDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminBookingDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminBookingDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminBookingDto>("booking", updateModel);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            var apiResponse = JsonConvert.DeserializeObject<ApiValidationResponseConstant>(await response.Content.ReadAsStringAsync());
            if (apiResponse?.Errors != null)
            {
                foreach (var errorItems in apiResponse.Errors)
                {
                    foreach (var errorItem in errorItems.Value)
                    {
                        responseMessage.ApiResponseMessage += errorItem + ',';
                    }
                }
            }
            else
            {
                if (!response.IsSuccessStatusCode)
                {

                    responseMessage.ApiResponseMessage = await response.Content.ReadAsStringAsync();
                }
            }

            return responseMessage;
        }

        public async Task DeleteDataAsync(int id)
        {
            var response = await _httpClient.DeleteAsync("booking?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
