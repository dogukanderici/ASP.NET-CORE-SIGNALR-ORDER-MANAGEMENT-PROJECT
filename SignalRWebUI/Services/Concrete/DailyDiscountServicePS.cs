using AutoMapper;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.DailyDiscountDtos;
using SignalRWebUI.Areas.Admin.Dtos.DailyDiscountDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class DailyDiscountServicePS : IDailyDiscountPS
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public DailyDiscountServicePS(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }
        public async Task<IHttpResponseMessageSettings<List<ResultDailyDiscountDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultDailyDiscountDto>> responseMessage = new HttpResponseMessageSettings<List<ResultDailyDiscountDto>>();

            var response = await _httpClient.GetAsync("dailydiscount");

            List<ResultDailyDiscountDto> values = new List<ResultDailyDiscountDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultDailyDiscountDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultDailyDiscountDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultDailyDiscountDto> responseMessage = new HttpResponseMessageSettings<ResultDailyDiscountDto>();

            var response = await _httpClient.GetAsync("dailydiscount/getdailydiscountbyid?id=" + id);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultDailyDiscountDto value = await response.Content.ReadFromJsonAsync<ResultDailyDiscountDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminDailyDiscountDto>> CreateDataAsync(CreateAdminDailyDiscountDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminDailyDiscountDto> responseMessage = new HttpResponseMessageSettings<CreateAdminDailyDiscountDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminDailyDiscountDto>("dailydiscount", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminDailyDiscountDto>> UpdateDataAsync(UpdateAdminDailyDiscountDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminDailyDiscountDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminDailyDiscountDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminDailyDiscountDto>("dailydiscount", updateModel);

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
            var response = await _httpClient.DeleteAsync("dailydiscount?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
