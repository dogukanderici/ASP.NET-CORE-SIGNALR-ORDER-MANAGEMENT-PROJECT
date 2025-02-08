using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.MessageDtos;
using SignalRWebUI.Areas.Admin.Dtos.MessageDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class MessageServicePS : IMessageServicePS
    {
        private readonly HttpClient _httpClient;

        public MessageServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultMessageDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultMessageDto>> responseMessage = new HttpResponseMessageSettings<List<ResultMessageDto>>();

            var response = await _httpClient.GetAsync("messages");

            List<ResultMessageDto> values = new List<ResultMessageDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultMessageDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultMessageDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultMessageDto> responseMessage = new HttpResponseMessageSettings<ResultMessageDto>();

            var response = await _httpClient.GetAsync("messages/getmessagebyid?id=" + id);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultMessageDto value = await response.Content.ReadFromJsonAsync<ResultMessageDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultMessageDto>> GetDataByGuid(Guid id)
        {
            HttpResponseMessageSettings<ResultMessageDto> responseMessage = new HttpResponseMessageSettings<ResultMessageDto>();

            var response = await _httpClient.GetAsync("messages/getmessagebyid?id=" + id);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultMessageDto value = await response.Content.ReadFromJsonAsync<ResultMessageDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultMessageDto>>> GetAllInboxMessageAsync(string receiverMail)
        {
            HttpResponseMessageSettings<List<ResultMessageDto>> responseMessage = new HttpResponseMessageSettings<List<ResultMessageDto>>();

            var response = await _httpClient.GetAsync("messages/getinboxmessage?receiverMail=" + receiverMail);

            List<ResultMessageDto> values = new List<ResultMessageDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultMessageDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultMessageDto>>> GetAllOutboxMessageAsync(string senderMail)
        {
            HttpResponseMessageSettings<List<ResultMessageDto>> responseMessage = new HttpResponseMessageSettings<List<ResultMessageDto>>();

            var response = await _httpClient.GetAsync("messages/getoutboxmessage?senderMail=" + senderMail);

            List<ResultMessageDto> values = new List<ResultMessageDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultMessageDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultMessageDto>>> GetAllMainMessageAsync(Guid mainId)
        {
            HttpResponseMessageSettings<List<ResultMessageDto>> responseMessage = new HttpResponseMessageSettings<List<ResultMessageDto>>();

            var response = await _httpClient.GetAsync("messages/getmessagebymainid?id=" + mainId);

            List<ResultMessageDto> values = new List<ResultMessageDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultMessageDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminMessageDto>> CreateDataAsync(CreateAdminMessageDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminMessageDto> responseMessage = new HttpResponseMessageSettings<CreateAdminMessageDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminMessageDto>("messages", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminMessageDto>> UpdateDataAsync(UpdateAdminMessageDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminMessageDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminMessageDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminMessageDto>("messages", updateModel);

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
            var response = await _httpClient.DeleteAsync("messages?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
