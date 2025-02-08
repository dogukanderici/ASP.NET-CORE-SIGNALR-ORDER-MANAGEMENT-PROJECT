using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.BookingDtos;
using SignalR.Dtos.Dtos.HelpDeskDtos;
using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class HelpDeskServicePS : IHelpDeskServicePS
    {
        private readonly HttpClient _httpClient;

        public HelpDeskServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultHelpDeskDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultHelpDeskDto>> responseMessage = new HttpResponseMessageSettings<List<ResultHelpDeskDto>>();

            var response = await _httpClient.GetAsync("helpdesks/gethelpdeskforinboxbyid");

            List<ResultHelpDeskDto> values = new List<ResultHelpDeskDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultHelpDeskDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultHelpDeskDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultHelpDeskDto> responseMessage = new HttpResponseMessageSettings<ResultHelpDeskDto>();

            var response = await _httpClient.GetAsync($"helpdesks/gethelpdeskbyid?id={id}");

            ResultHelpDeskDto value = new ResultHelpDeskDto();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                value = await response.Content.ReadFromJsonAsync<ResultHelpDeskDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultHelpDeskDto>>> GetDataAdminOutboxByGuidAsync(Guid id)
        {
            HttpResponseMessageSettings<List<ResultHelpDeskDto>> responseMessage = new HttpResponseMessageSettings<List<ResultHelpDeskDto>>();

            var response = await _httpClient.GetAsync($"helpdesks/gethelpdeskforoutboxbyid?id={id}");

            List<ResultHelpDeskDto> values = new List<ResultHelpDeskDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultHelpDeskDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultHelpDeskDto>>> GetDataUserOutboxByGuidAsync(string userMail)
        {
            HttpResponseMessageSettings<List<ResultHelpDeskDto>> responseMessage = new HttpResponseMessageSettings<List<ResultHelpDeskDto>>();

            var response = await _httpClient.GetAsync($"helpdesks/gethelpdeskforuseroutboxbyid?usermail={userMail}");

            List<ResultHelpDeskDto> values = new List<ResultHelpDeskDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultHelpDeskDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultHelpDeskDto>>> GetDataUserInboxByGuidAsync(string userMail, Guid replyId)
        {
            HttpResponseMessageSettings<List<ResultHelpDeskDto>> responseMessage = new HttpResponseMessageSettings<List<ResultHelpDeskDto>>();

            var response = await _httpClient.GetAsync($"helpdesks/gethelpdeskforuserinboxbyid?usermail={userMail}&replyid={replyId}");

            List<ResultHelpDeskDto> values = new List<ResultHelpDeskDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultHelpDeskDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultHelpDeskDto>> GetDataByGuidAsync(Guid id)
        {
            HttpResponseMessageSettings<ResultHelpDeskDto> responseMessage = new HttpResponseMessageSettings<ResultHelpDeskDto>();

            var response = await _httpClient.GetAsync($"helpdesks/gethelpdeskbyid?id={id}");

            ResultHelpDeskDto value = new ResultHelpDeskDto();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                value = await response.Content.ReadFromJsonAsync<ResultHelpDeskDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminHelpDeskDto>> CreateDataAsync(CreateAdminHelpDeskDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminHelpDeskDto> responseMessage = new HttpResponseMessageSettings<CreateAdminHelpDeskDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminHelpDeskDto>("helpdesks", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminHelpDeskDto>> UpdateDataAsync(UpdateAdminHelpDeskDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminHelpDeskDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminHelpDeskDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminHelpDeskDto>("helpdesks", updateModel);

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
            var response = await _httpClient.DeleteAsync($"helpdesks?id={id}");

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }

        public async Task DeleteDataByGuidAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"helpdesks?id={id}");

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
