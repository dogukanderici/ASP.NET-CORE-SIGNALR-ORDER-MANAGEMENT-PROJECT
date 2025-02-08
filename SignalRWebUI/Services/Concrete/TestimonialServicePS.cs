using AutoMapper;
using Newtonsoft.Json;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.TestimonialDtos;
using SignalRWebUI.Areas.Admin.Dtos.TestimonialDtos;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Services.Concrete
{
    public class TestimonialServicePS : ITestimonialServicePS
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public TestimonialServicePS(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<IHttpResponseMessageSettings<List<ResultTestimonialDto>>> GetAllDataAsync(string? endPoint)
        {
            HttpResponseMessageSettings<List<ResultTestimonialDto>> responseMessage = new HttpResponseMessageSettings<List<ResultTestimonialDto>>();

            var response = await _httpClient.GetAsync("testimonial");

            List<ResultTestimonialDto> values = new List<ResultTestimonialDto>();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                values = await response.Content.ReadFromJsonAsync<List<ResultTestimonialDto>>();
                responseMessage.GetData = values;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ResultTestimonialDto>> GetDataAsync(int id)
        {
            HttpResponseMessageSettings<ResultTestimonialDto> responseMessage = new HttpResponseMessageSettings<ResultTestimonialDto>();

            var response = await _httpClient.GetAsync($"testimonial/gettestimonialbyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ResultTestimonialDto value = await response.Content.ReadFromJsonAsync<ResultTestimonialDto>();
                responseMessage.GetData = value;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<CreateAdminTestimonialDto>> CreateDataAsync(CreateAdminTestimonialDto createMdoel)
        {
            HttpResponseMessageSettings<CreateAdminTestimonialDto> responseMessage = new HttpResponseMessageSettings<CreateAdminTestimonialDto>();

            var response = await _httpClient.PostAsJsonAsync<CreateAdminTestimonialDto>("testimonial", createMdoel);

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

        public async Task<IHttpResponseMessageSettings<UpdateAdminTestimonialDto>> UpdateDataAsync(UpdateAdminTestimonialDto updateModel)
        {
            HttpResponseMessageSettings<UpdateAdminTestimonialDto> responseMessage = new HttpResponseMessageSettings<UpdateAdminTestimonialDto>();

            var response = await _httpClient.PutAsJsonAsync<UpdateAdminTestimonialDto>("testimonial", updateModel);

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
            var response = await _httpClient.DeleteAsync("testimonial?id=" + id);

            int statusCode = Convert.ToInt32(response.StatusCode);
            HttpStatusCode statusMessage = response.StatusCode;
        }
    }
}
