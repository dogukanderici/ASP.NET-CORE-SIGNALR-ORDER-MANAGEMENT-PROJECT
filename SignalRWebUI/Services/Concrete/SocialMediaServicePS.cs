using AutoMapper;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.SocialMediaDtos;
using SignalRWebUI.Areas.Admin.Dtos.SocialMediaDtos;
using SignalRWebUI.Areas.Admin.Dtos.SocialMediaDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class SocialMediaServicePS : ISocialMediaServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public SocialMediaServicePS(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultSocialMediaDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultSocialMediaDto>> responseMessage = new HttpResponseMessageSettings<List<ResultSocialMediaDto>>();

            var response = await _httpClient.GetAsync("socialmedia");

            List<ResultSocialMediaDto> values = new List<ResultSocialMediaDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultSocialMediaDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultSocialMediaDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultSocialMediaDto> responseMessage = new HttpResponseMessageSettings<ResultSocialMediaDto>();

            var response = await _httpClient.GetAsync($"socialmedia/getsocialmediabyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultSocialMediaDto value = await response.Content.ReadFromJsonAsync<ResultSocialMediaDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminSocialMediaDto>> CreateDataAsync(CreateAdminSocialMediaDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminSocialMediaDto> responseMessage = new HttpResponseMessageSettings<CreateAdminSocialMediaDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminSocialMediaDto>("socialmedia", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminSocialMediaDto>> UpdateDataAsync(UpdateAdminSocialMediaDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminSocialMediaDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminSocialMediaDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminSocialMediaDto>("socialmedia", updateModel);

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
            var response = await _httpClient.DeleteAsync("socialmedia?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
