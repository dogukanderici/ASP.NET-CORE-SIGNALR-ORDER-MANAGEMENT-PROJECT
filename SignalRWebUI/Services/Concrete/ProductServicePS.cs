using AutoMapper;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.ProductDtos;
using SignalRWebUI.Areas.Admin.Dtos.ProductDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class ProductServicePS : IProductServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ProductServicePS(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultProductDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultProductDto>> responseMessage = new HttpResponseMessageSettings<List<ResultProductDto>>();

            var response = await _httpClient.GetAsync(endPoint != null ? endPoint : "product");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                string x = await response.Content.ReadAsStringAsync();

                if (endPoint == null)
                {
                    List<ResultProductDto> values = await response.Content.ReadFromJsonAsync<List<ResultProductDto>>();
                    responseMessage.GetData = values;
                }
                else
                {
                    PagingDatasViewModel<ResultProductDto> values = await response.Content.ReadFromJsonAsync<PagingDatasViewModel<ResultProductDto>>();
                    responseMessage.GetData = values.Data;
                }
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultProductDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultProductDto> responseMessage = new HttpResponseMessageSettings<ResultProductDto>();

            var response = await _httpClient.GetAsync("product/getproductbyid?id=" + id);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultProductDto value = await response.Content.ReadFromJsonAsync<ResultProductDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultProductDto>>> GetAllDataByCategoryAsync(int id)
        {
            HttpResponseMessageSettings<List<ResultProductDto>> responseMessage = new HttpResponseMessageSettings<List<ResultProductDto>>();

            var response = (id != 0 ? await _httpClient.GetAsync("product/getproductswithcategories?id=" + id) : await _httpClient.GetAsync("product"));

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                List<ResultProductDto> value = await response.Content.ReadFromJsonAsync<List<ResultProductDto>>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultProductDto>>> GetAllDataByCountAsync(int dataCount)
        {
            HttpResponseMessageSettings<List<ResultProductDto>> responseMessage = new HttpResponseMessageSettings<List<ResultProductDto>>();

            var response = await _httpClient.GetAsync("product/getproductswithcategoriesbycount?datacount=" + dataCount);

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                List<ResultProductDto> value = await response.Content.ReadFromJsonAsync<List<ResultProductDto>>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<int>> GetProductCount()
        {
            HttpResponseMessageSettings<int> responseMessage = new HttpResponseMessageSettings<int>();

            var response = await _httpClient.GetAsync("product/getproductcount");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                int value = await response.Content.ReadFromJsonAsync<int>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminProductDto>> CreateDataAsync(CreateAdminProductDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminProductDto> responseMessage = new HttpResponseMessageSettings<CreateAdminProductDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminProductDto>("product", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminProductDto>> UpdateDataAsync(UpdateAdminProductDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminProductDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminProductDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminProductDto>("product", updateModel);

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
            var response = await _httpClient.DeleteAsync("product?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
