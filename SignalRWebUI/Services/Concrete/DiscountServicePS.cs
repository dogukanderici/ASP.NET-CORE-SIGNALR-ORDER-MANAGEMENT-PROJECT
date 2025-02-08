using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.Discounts;
using SignalRWebUI.Areas.Admin.Dtos.DiscountDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class DiscountServicePS : IDiscountServicePS
    {
        private readonly HttpClient _httpClient;

        public DiscountServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultDiscountDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultDiscountDto>> responseMessage = new HttpResponseMessageSettings<List<ResultDiscountDto>>();

            var response = await _httpClient.GetAsync("discounts");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<List<ResultDiscountDto>>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultDiscountDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultDiscountDto> responseMessage = new HttpResponseMessageSettings<ResultDiscountDto>();

            var response = await _httpClient.GetAsync($"discounts/getdiscountbyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultDiscountDto>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultDiscountDto>> GetDataByGuidAsync(Guid id)
        {
            HttpResponseMessageSettings<ResultDiscountDto> responseMessage = new HttpResponseMessageSettings<ResultDiscountDto>();

            var response = await _httpClient.GetAsync($"discounts/getdiscountbyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultDiscountDto>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultDiscountDto>> GetDataByTitleAsync(string title)
        {
            HttpResponseMessageSettings<ResultDiscountDto> responseMessage = new HttpResponseMessageSettings<ResultDiscountDto>();

            var response = await _httpClient.GetAsync($"discounts/getdiscountbytitle?title={title}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultDiscountDto>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminDiscountDto>> CreateDataAsync(CreateAdminDiscountDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminDiscountDto> responseMessage = new HttpResponseMessageSettings<CreateAdminDiscountDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminDiscountDto>("discounts", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminDiscountDto>> UpdateDataAsync(UpdateAdminDiscountDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminDiscountDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminDiscountDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminDiscountDto>("discounts", updateModel);

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

            var response = await _httpClient.DeleteAsync($"discounts?id={id}");

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }

        public async Task DeleteDataByGuidAsync(Guid id)
        {

            var response = await _httpClient.DeleteAsync($"discounts?id={id}");

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
