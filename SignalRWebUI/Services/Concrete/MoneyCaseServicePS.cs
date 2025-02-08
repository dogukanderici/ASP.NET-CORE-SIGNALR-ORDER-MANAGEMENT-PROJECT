using SignalR.Dtos.Dtos.MoneyCaseDtos;
using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;
using SignalRWebUI.Areas.Admin.Dtos.MoneyCaseDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class MoneyCaseServicePS : IMoneyCaseServicePS
    {
        private readonly HttpClient _httpClient;

        public MoneyCaseServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultMoneyCaseDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultMoneyCaseDto>> responseMessage = new HttpResponseMessageSettings<List<ResultMoneyCaseDto>>();

            var response = await _httpClient.GetAsync("moneycases");

            List<ResultMoneyCaseDto> values = new List<ResultMoneyCaseDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultMoneyCaseDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultMoneyCaseDto>>> GetAllOutcomeDataAsync()
        {
            HttpResponseMessageSettings<List<ResultMoneyCaseDto>> responseMessage = new HttpResponseMessageSettings<List<ResultMoneyCaseDto>>();

            var response = await _httpClient.GetAsync("moneycases/getalloutcomedata");

            List<ResultMoneyCaseDto> values = new List<ResultMoneyCaseDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultMoneyCaseDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultMoneyCaseDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultMoneyCaseDto> responseMessage = new HttpResponseMessageSettings<ResultMoneyCaseDto>();

            var response = await _httpClient.GetAsync($"moneycases/getmoneycasebyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultMoneyCaseDto value = await response.Content.ReadFromJsonAsync<ResultMoneyCaseDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminMoneyCaseDto>> CreateDataAsync(CreateAdminMoneyCaseDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminMoneyCaseDto> responseMessage = new HttpResponseMessageSettings<CreateAdminMoneyCaseDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminMoneyCaseDto>("moneycases", createMdoel);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (!response.IsSuccessStatusCode)
            {

                responseMessage.ApiResponseMessage = await response.Content.ReadAsStringAsync();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<UpdateAdminMoneyCaseDto>> UpdateDataAsync(UpdateAdminMoneyCaseDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminMoneyCaseDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminMoneyCaseDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminMoneyCaseDto>("moneycases", updateModel);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (!response.IsSuccessStatusCode)
            {

                responseMessage.ApiResponseMessage = await response.Content.ReadAsStringAsync();
            }

            return responseMessage;
        }

        public async Task DeleteDataAsync(int id)
        {
            var response = await _httpClient.DeleteAsync("moneycases?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
