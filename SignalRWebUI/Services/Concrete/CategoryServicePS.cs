using AutoMapper;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.CategoryDtos;
using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class CategoryServicePS : ICategoryServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public CategoryServicePS(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultCategoryDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultCategoryDto>> responseMessage = new HttpResponseMessageSettings<List<ResultCategoryDto>>();

            var response = await _httpClient.GetAsync("category");

            List<ResultCategoryDto> values = new List<ResultCategoryDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultCategoryDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultCategoryDto> responseMessage = new HttpResponseMessageSettings<ResultCategoryDto>();

            var response = await _httpClient.GetAsync("category/getcategorybyid?id=" + id);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultCategoryDto value = await response.Content.ReadFromJsonAsync<ResultCategoryDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminCategoryDto>> CreateDataAsync(CreateAdminCategoryDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminCategoryDto> responseMessage = new HttpResponseMessageSettings<CreateAdminCategoryDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminCategoryDto>("category", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminCategoryDto>> UpdateDataAsync(UpdateAdminCategoryDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminCategoryDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminCategoryDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminCategoryDto>("category", updateModel);

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
            var response = await _httpClient.DeleteAsync("category?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
