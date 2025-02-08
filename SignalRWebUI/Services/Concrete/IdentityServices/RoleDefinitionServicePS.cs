using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SignalR.IdentityServerApi.Settings;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Settings;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SignalRWebUI.Services.Concrete.IdentityServices
{
    public class RoleDefinitionServicePS : IRoleDefinitionServicePS
    {
        private readonly HttpClient _httpClient;

        public RoleDefinitionServicePS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IHttpResponseMessageSettings<List<IdentityRole>>> GetAllRoleListAsync()
        {
            HttpResponseMessageSettings<List<IdentityRole>> responseMessage = new HttpResponseMessageSettings<List<IdentityRole>>();

            var response = await _httpClient.GetAsync("roledefinitions");

            responseMessage.StatusCode = Convert.ToInt32(response.StatusCode);
            responseMessage.StatusMessage = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                List<IdentityRole> apiResponse = await response.Content.ReadFromJsonAsync<List<IdentityRole>>();
                responseMessage.GetData = apiResponse;
            }

            return responseMessage;
        }

        public async Task<IHttpResponseMessageSettings<string>> AddNewRole(string newRoleName)
        {
            HttpResponseMessageSettings<string> responseMessage = new HttpResponseMessageSettings<string>();

            //string jsonString = JsonConvert.SerializeObject(new { newRoleName = newRoleName });

            //StringContent jsonStringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _httpClient.GetAsync($"roledefinitions/addnewrole?newrolename={newRoleName}");

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
