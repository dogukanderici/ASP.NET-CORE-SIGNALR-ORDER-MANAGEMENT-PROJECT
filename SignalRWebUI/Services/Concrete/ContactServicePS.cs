using AutoMapper;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.ContactDtos;
using SignalRWebUI.Areas.Admin.Dtos.ContactDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class ContactServicePS : IContactServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ContactServicePS(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultContactDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultContactDto>> responseMessage = new HttpResponseMessageSettings<List<ResultContactDto>>();

            var response = await _httpClient.GetAsync("contact");

            List<ResultContactDto> values = new List<ResultContactDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultContactDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultContactDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultContactDto> responseMessage = new HttpResponseMessageSettings<ResultContactDto>();

            var response = await _httpClient.GetAsync("contact/getcontactbyid?id=" + id);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultContactDto value = await response.Content.ReadFromJsonAsync<ResultContactDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminContactDto>> CreateDataAsync(CreateAdminContactDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminContactDto> responseMessage = new HttpResponseMessageSettings<CreateAdminContactDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminContactDto>("contact", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminContactDto>> UpdateDataAsync(UpdateAdminContactDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminContactDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminContactDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminContactDto>("contact", updateModel);

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
            var response = await _httpClient.DeleteAsync("contact?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
