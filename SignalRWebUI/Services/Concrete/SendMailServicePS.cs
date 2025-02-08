using SignalRWebUI.Areas.Admin.Dtos.SendMailDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net.Http.Json;

namespace SignalRWebUI.Services.Concrete
{
    public class SendMailServicePS : ISendMailServicePS
    {
        private readonly HttpClient _httpClient;

        public SendMailServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<string>> SendMailAsync(CreateAdminMailDto createAdminMailDto)
        {
            HttpResponseMessageSettings<string> responseMessage = new Settings.HttpResponseMessageSettings<string>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminMailDto>("sendmails", createAdminMailDto);

            string responseRequest = await response.Content.ReadAsStringAsync();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;
            responseMessage.GetData = responseRequest;

            return responseMessage;
        }
    }
}
