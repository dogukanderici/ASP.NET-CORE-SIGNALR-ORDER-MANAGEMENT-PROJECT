using AutoMapper;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.WorkingHourDtos;
using SignalRWebUI.Areas.Admin.Dtos.WorkingHourDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class WorkingHourServicePS : IWorkingHourServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public WorkingHourServicePS(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultWorkingHourDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultWorkingHourDto>> responseMessage = new HttpResponseMessageSettings<List<ResultWorkingHourDto>>();

            var response = await _httpClient.GetAsync("workinghour");

            List<ResultWorkingHourDto> values = new List<ResultWorkingHourDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultWorkingHourDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultWorkingHourDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultWorkingHourDto> responseMessage = new HttpResponseMessageSettings<ResultWorkingHourDto>();

            var response = await _httpClient.GetAsync($"workinghour/getworkinghourbyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultWorkingHourDto value = await response.Content.ReadFromJsonAsync<ResultWorkingHourDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminWorkingHourDto>> CreateDataAsync(CreateAdminWorkingHourDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminWorkingHourDto> responseMessage = new HttpResponseMessageSettings<CreateAdminWorkingHourDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminWorkingHourDto>("workinghour", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminWorkingHourDto>> UpdateDataAsync(UpdateAdminWorkingHourDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminWorkingHourDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminWorkingHourDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminWorkingHourDto>("workinghour", updateModel);

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
            var response = await _httpClient.DeleteAsync("workinghour?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
