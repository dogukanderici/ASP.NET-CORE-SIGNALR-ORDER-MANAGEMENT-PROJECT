using SignalR.IdentityServerApi.Models;
using SignalRWebUI.Dtos.IdentityDtos;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Concrete.IdentityServices
{
    public class UserProfileChangeServicePS : IUserProfileChangeServicePS
    {
        private readonly HttpClient _httpClient;

        public UserProfileChangeServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<ApplicationUser>> GetUserInfoAsync(string userid)
        {
            HttpResponseMessageSettings<ApplicationUser> responseMessage = new HttpResponseMessageSettings<ApplicationUser>();

            var response = await _httpClient.GetAsync($"settings/getuserinfo?userid={userid}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                ApplicationUser apiResponse = await response.Content.ReadFromJsonAsync<ApplicationUser>();
                responseMessage.GetData = apiResponse;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ApplicationUser>> ChangeUserInfoAsync(UserEditDto userEditDto)
        {
            HttpResponseMessageSettings<ApplicationUser> responseMessage = new HttpResponseMessageSettings<ApplicationUser>();

            var response = await _httpClient.PostAsJsonAsync<UserEditDto>("settings/editpersonalinfo", userEditDto);

            string responseString = await response.Content.ReadAsStringAsync();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<ApplicationUser>> ChangeUserPasswordAsync(UserEditDto userEditDto)
        {
            HttpResponseMessageSettings<ApplicationUser> responseMessage = new HttpResponseMessageSettings<ApplicationUser>();

            var response = await _httpClient.PostAsJsonAsync<UserEditDto>("settings/changepassword", userEditDto);

            string responseString = await response.Content.ReadAsStringAsync();

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
            }

            return responseMessage;
        }
    }
}
