using Azure;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.RestaurantTableDtos;
using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class RestaurantTableServicePS : IRestaurantTableServicePS
    {
        private readonly HttpClient _httpClient;

        public RestaurantTableServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultRestaurantTableDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultRestaurantTableDto>> responseMessage = new HttpResponseMessageSettings<List<ResultRestaurantTableDto>>();

            var response = await _httpClient.GetAsync("restauranttables");

            responseMessage.StatusCode = Convert.ToInt32(responseMessage.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<List<ResultRestaurantTableDto>>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultRestaurantTableDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultRestaurantTableDto> responseMessage = new HttpResponseMessageSettings<ResultRestaurantTableDto>();

            var response = await _httpClient.GetAsync($"restauranttables/gettablebyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(responseMessage.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultRestaurantTableDto>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultRestaurantTableDto>> GetTableIdByTableName(string tableName)
        {
            HttpResponseMessageSettings<ResultRestaurantTableDto> responseMessage = new HttpResponseMessageSettings<ResultRestaurantTableDto>();

            var response = await _httpClient.GetAsync($"restauranttables/gettableidbytablename?tablename={tableName}");

            responseMessage.StatusCode = Convert.ToInt32(responseMessage.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultRestaurantTableDto>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminRestaurantTableDto>> CreateDataAsync(CreateAdminRestaurantTableDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminRestaurantTableDto> responseMessage = new HttpResponseMessageSettings<CreateAdminRestaurantTableDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminRestaurantTableDto>("restauranttables", createMdoel);

            responseMessage.StatusCode = Convert.ToInt32(responseMessage.StatusCode);
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

        public async Task<IHttpResponseMessageSettings<UpdateAdminRestaurantTableDto>> UpdateDataAsync(UpdateAdminRestaurantTableDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminRestaurantTableDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminRestaurantTableDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminRestaurantTableDto>("restauranttables", updateModel);

            responseMessage.StatusCode = Convert.ToInt32(responseMessage.StatusCode);
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
            var response = await _httpClient.DeleteAsync($"restauranttables?id={id}");

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
