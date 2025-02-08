using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Settings;
using System.Net;
using System.Text;

namespace SignalRWebUI.Services.Concrete.IdentityServices
{
    public class UserRoleDefinitionServicePS : IUserRoleDefinitonServicePS
    {
        private readonly HttpClient _httpClient;

        public UserRoleDefinitionServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<IdentityUser>>> GetAllUserAsync()
        {
            HttpResponseMessageSettings<List<IdentityUser>> responseMessage = new HttpResponseMessageSettings<List<IdentityUser>>();

            var response = await _httpClient.GetAsync("userroledefinitions");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<List<IdentityUser>>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<IdentityUser>> GetUserDataAsync(string id)
        {
            HttpResponseMessageSettings<IdentityUser> responseMessage = new HttpResponseMessageSettings<IdentityUser>();

            var response = await _httpClient.GetAsync($"userroledefinitions/getuserdatabyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<IdentityUser>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<List<string>>> GetUserAsync(string id)
        {
            HttpResponseMessageSettings<List<string>> responseMessage = new HttpResponseMessageSettings<List<string>>();

            var response = await _httpClient.GetAsync($"userroledefinitions/getuserbyid?id={id}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadFromJsonAsync<List<string>>();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<string>> CreateNewUserRole(string id, string newUserRole)
        {
            HttpResponseMessageSettings<string> responseMessage = new HttpResponseMessageSettings<string>();

            string jsonString = JsonConvert.SerializeObject(new { id = id, newUserRole = newUserRole });

            StringContent jsonStringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _httpClient.GetAsync($"userroledefinitions/addnewroleuser/{id}/{newUserRole}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadAsStringAsync();
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<string>> DeleteUserRole(string id, string userRoleName)
        {
            HttpResponseMessageSettings<string> responseMessage = new HttpResponseMessageSettings<string>();

            var response = await _httpClient.GetAsync($"userroledefinitions/deleteroleuser/{id}/{userRoleName}");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                responseMessage.GetData = await response.Content.ReadAsStringAsync();
            }

            return responseMessage;
        }
    }
}
