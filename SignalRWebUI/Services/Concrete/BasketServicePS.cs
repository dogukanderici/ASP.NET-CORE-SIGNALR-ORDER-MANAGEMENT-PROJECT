using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.BasketDtos;
using SignalRWebUI.Areas.Admin.Dtos.BasketDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class BasketServicePS : IBasketServicePS
    {
        private readonly HttpClient _httpClient;

        public BasketServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultBasketDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultBasketDto>> responseMessage = new HttpResponseMessageSettings<List<ResultBasketDto>>();

            var response = await _httpClient.GetAsync($"basket?restauranttableid={endPoint}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<List<ResultBasketDto>>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultBasketDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultBasketDto> responseMessage = new HttpResponseMessageSettings<ResultBasketDto>();

            var response = await _httpClient.GetAsync($"basket/getbasketbytableid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultBasketDto>();
            }

            return responseMessage;
        }
        public async Task<IHttpResponseMessageSettings<CreateAdminBasketDto>> CreateDataAsync(CreateAdminBasketDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminBasketDto> responseMessage = new HttpResponseMessageSettings<CreateAdminBasketDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminBasketDto>("basket", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminBasketDto>> UpdateDataAsync(UpdateAdminBasketDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminBasketDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminBasketDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminBasketDto>("basket", updateModel);

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
            var response = await _httpClient.DeleteAsync("basket?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }

        public async Task DeleteBasketItemAsync(int id, int tableID)
        {
            var response = await _httpClient.DeleteAsync($"basket/deletebasketitem?id={id}&tableid={tableID}");

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
