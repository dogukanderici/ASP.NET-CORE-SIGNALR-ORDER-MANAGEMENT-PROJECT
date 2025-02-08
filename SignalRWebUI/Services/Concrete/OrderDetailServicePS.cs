using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.OrderDetailDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDetailDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class OrderDetailServicePS : IOrderDetailServicePS
    {
        private readonly HttpClient _httpClient;

        public OrderDetailServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultOrderDetailDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultOrderDetailDto>> responseMessage = new HttpResponseMessageSettings<List<ResultOrderDetailDto>>();

            var response = await _httpClient.GetAsync("orderdetails");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<List<ResultOrderDetailDto>>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultOrderDetailDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultOrderDetailDto> responseMessage = new HttpResponseMessageSettings<ResultOrderDetailDto>();

            var response = await _httpClient.GetAsync($"orderdetails/getorderdetaildatabyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<ResultOrderDetailDto>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultOrderDetailDto>>> GetAllDataByOrderIdAsync(Guid id)
        {
            HttpResponseMessageSettings<List<ResultOrderDetailDto>> responseMessage = new HttpResponseMessageSettings<List<ResultOrderDetailDto>>();

            var response = await _httpClient.GetAsync($"orderdetails/getorderdetaildatabyiorderid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<List<ResultOrderDetailDto>>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminOrderDetailDto>> CreateDataAsync(CreateAdminOrderDetailDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminOrderDetailDto> responseMessage = new HttpResponseMessageSettings<CreateAdminOrderDetailDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminOrderDetailDto>("orderdetails", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminOrderDetailDto>> UpdateDataAsync(UpdateAdminOrderDetailDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminOrderDetailDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminOrderDetailDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminOrderDetailDto>("orderdetails", updateModel);

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
            HttpResponseMessageSettings<UpdateAdminOrderDetailDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminOrderDetailDto>();

            var response = await _httpClient.DeleteAsync($"orderdetails?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.ApiResponseMessage = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
