using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.OrderDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class OrderServicePS : IOrderServicePS
    {
        private readonly HttpClient _httpClient;

        public OrderServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultOrderDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultOrderDto>> responseMessage = new HttpResponseMessageSettings<List<ResultOrderDto>>();

            var response = await _httpClient.GetAsync("orders");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<List<ResultOrderDto>>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultOrderDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultOrderDto> responseMessage = new HttpResponseMessageSettings<ResultOrderDto>();

            var response = await _httpClient.GetAsync($"orders/getorderbyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultOrderDto>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultOrderDto>> GetDataByTableAsync(string table)
        {
            HttpResponseMessageSettings<ResultOrderDto> responseMessage = new HttpResponseMessageSettings<ResultOrderDto>();

            var response = await _httpClient.GetAsync($"orders/getorderbytable?id={table}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultOrderDto>();
            }

            return responseMessage;
        }
        public async Task<IHttpResponseMessageSettings<CreateAdminOrderDto>> CreateDataAsync(CreateAdminOrderDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminOrderDto> responseMessage = new HttpResponseMessageSettings<CreateAdminOrderDto>();

            createMdoel.OrderID = Guid.NewGuid();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminOrderDto>("orders", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminOrderDto>> UpdateDataAsync(UpdateAdminOrderDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminOrderDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminOrderDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminOrderDto>("orders", updateModel);

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
            HttpResponseMessageSettings<bool> responseMessage = new HttpResponseMessageSettings<bool>();

            var response = await _httpClient.DeleteAsync($"orders?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.ApiResponseMessage = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
