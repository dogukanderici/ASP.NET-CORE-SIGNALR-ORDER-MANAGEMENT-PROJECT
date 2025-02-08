using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.FeatureDtos;
using SignalRWebUI.Areas.Admin.Dtos.AboutDtos;
using SignalRWebUI.Areas.Admin.Dtos.FeatureDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class DefaultFeatureService : IDefaultFeatureServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public DefaultFeatureService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultFeatureDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultFeatureDto>> responseMessage = new HttpResponseMessageSettings<List<ResultFeatureDto>>();

            var response = await _httpClient.GetAsync("feature");

            List<ResultFeatureDto> value = new List<ResultFeatureDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                value = await response.Content.ReadFromJsonAsync<List<ResultFeatureDto>>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultFeatureDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultFeatureDto> responseMessage = new HttpResponseMessageSettings<ResultFeatureDto>();

            var response = await _httpClient.GetAsync("feature/getfeaturebyid?id=" + id);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultFeatureDto value = await response.Content.ReadFromJsonAsync<ResultFeatureDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminFeatureDto>> CreateDataAsync(CreateAdminFeatureDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminFeatureDto> responseMessage = new HttpResponseMessageSettings<CreateAdminFeatureDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminFeatureDto>("feature", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminFeatureDto>> UpdateDataAsync(UpdateAdminFeatureDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminFeatureDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminFeatureDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminFeatureDto>("feature", updateModel);

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
            var response = await _httpClient.DeleteAsync("feature?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
