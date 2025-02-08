using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.Core.Constants;
using SignalR.Dtos.Dtos.IdentityDtos;
using SignalR.Entities.Concrete;
using SignalRWebUI.Models;
using System.Net.Http.Json;

namespace SignalRWebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public RegisterController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterViewModel registerViewModel)
        {
            var appUser = new RegisterDto()
            {
                Name = registerViewModel.RegisterProperty.Name,
                Surname = registerViewModel.RegisterProperty.Surname,
                Username = registerViewModel.RegisterProperty.Username,
                Email = registerViewModel.RegisterProperty.Email
            };

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync<RegisterDto>("http://localhost:5196/api/registers", registerViewModel.RegisterProperty);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<ApiResponseConstant>();

                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
