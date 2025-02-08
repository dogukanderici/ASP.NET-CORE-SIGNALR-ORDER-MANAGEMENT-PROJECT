using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.AboutDtos;
using SignalRWebUI.Areas.Admin.Dtos.AboutDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class DefaultAboutService : IDefaultAboutServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly IClientCredentialsTokenService _clientCredentialsTokenService;
        private readonly ApiSettings _apiSettings;

        public DefaultAboutService(HttpClient httpClient, IClientCredentialsTokenService clientCredentialsTokenService, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _clientCredentialsTokenService = clientCredentialsTokenService;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultAboutDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultAboutDto>> reponseMessage = new HttpResponseMessageSettings<List<ResultAboutDto>>();

            var response = await _httpClient.GetAsync("about");

            List<ResultAboutDto> value = new List<ResultAboutDto>();

            reponseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            reponseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                value = await response.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
                reponseMessage.GetData = value;
            }

            return reponseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultAboutDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultAboutDto> reponseMessage = new HttpResponseMessageSettings<ResultAboutDto>();

            var response = await _httpClient.GetAsync("about/getaboutbyid?id=" + id);

            reponseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            reponseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                var stringData = await response.Content.ReadAsStringAsync();

                ResultAboutDto value = await response.Content.ReadFromJsonAsync<ResultAboutDto>();
                reponseMessage.GetData = value;
            }

            return reponseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminAboutDto>> CreateDataAsync(CreateAdminAboutDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminAboutDto> responseMessage = new HttpResponseMessageSettings<CreateAdminAboutDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminAboutDto>("about", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminAboutDto>> UpdateDataAsync(UpdateAdminAboutDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminAboutDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminAboutDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminAboutDto>("about", updateModel);

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

            var response = await _httpClient.DeleteAsync("about?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
