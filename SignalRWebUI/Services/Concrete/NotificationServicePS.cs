using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.NotificationDtos;
using SignalRWebUI.Areas.Admin.Dtos.NotificationDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class NotificationServicePS : INotificationServicePS
    {
        private readonly HttpClient _httpClient;

        public NotificationServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultNotificationDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultNotificationDto>> responseMessage = new HttpResponseMessageSettings<List<ResultNotificationDto>>();

            var response = await _httpClient.GetAsync("notifications");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                List<ResultNotificationDto> values = await response.Content.ReadFromJsonAsync<List<ResultNotificationDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public Task<IHttpResponseMessageSettings<ResultNotificationDto>> GetDataAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IHttpResponseMessageSettings<ResultNotificationDto>> GetDataWithStatusAsync(int id, bool status)
        {
            HttpResponseMessageSettings<ResultNotificationDto> responseMessage = new HttpResponseMessageSettings<ResultNotificationDto>();

            var response = await _httpClient.GetAsync($"notifications/getnotificationsbyid?id={id}&status={status}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultNotificationDto values = await response.Content.ReadFromJsonAsync<ResultNotificationDto>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminNotificationDto>> CreateDataAsync(CreateAdminNotificationDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminNotificationDto> responseMessage = new HttpResponseMessageSettings<CreateAdminNotificationDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminNotificationDto>("notifications", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminNotificationDto>> UpdateDataAsync(UpdateAdminNotificationDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminNotificationDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminNotificationDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminNotificationDto>("notifications", updateModel);

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

        public async Task<IHttpResponseMessageSettings<ResultNotificationDto>> UpdateDataWithStatusAsync(int id, bool status)
        {
            HttpResponseMessageSettings<ResultNotificationDto> responseMessage = new HttpResponseMessageSettings<ResultNotificationDto>();

            var response = await _httpClient.GetAsync($"notifications/changenotificationstatus?id={id}&status={status}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultNotificationDto values = await response.Content.ReadFromJsonAsync<ResultNotificationDto>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task DeleteDataAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"notifications?id={id}");

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
