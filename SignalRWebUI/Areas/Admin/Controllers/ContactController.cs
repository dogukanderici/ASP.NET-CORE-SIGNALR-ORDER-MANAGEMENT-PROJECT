using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.ContactDtos;
using SignalRWebUI.Areas.Admin.Dtos.ContactDtos;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    public class ContactController : AdminBaseController
    {
        private readonly IContactServicePS _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactServicePS contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "İletişim Bilgileri";
            ViewBag.EmptyDataInfo = "İletişim Bilgisi";

            var responseMessage = await _contactService.GetAllDataAsync(null);

            var model = new ContactViewModel();

            if (responseMessage.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = responseMessage.StatusMessage;
            }
            else
            {
                List<ResultAdminContactDto> values = _mapper.Map<List<ResultAdminContactDto>>(responseMessage.GetData);
                model.ResultContacts = values;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult CreateContact()
        {
            ViewBag.MainTitle = "İletişim Bilgileri";

            var model = new ContactViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateContact(ContactViewModel contactViewModel)
        {

            var response = await _contactService.CreateDataAsync(contactViewModel.CreateContact);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Kategori Eklenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            contactViewModel.HttpResponseMessage = response.StatusMessage;
            contactViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(contactViewModel);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateContact(int id)
        {
            ViewBag.MainTitle = "İletişim Bilgileri";

            var response = await _contactService.GetDataAsync(id);

            var model = new ContactViewModel();

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                UpdateAdminContactDto value = _mapper.Map<UpdateAdminContactDto>(response.GetData);
                model.UpdateContact = value;
            }
            else
            {
                model.HttpResponseMessage = response.StatusMessage;
                model.ApiResponseMessage = response.ApiResponseMessage;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateContact(ContactViewModel contactViewModel)
        {
            var response = await _contactService.UpdateDataAsync(contactViewModel.UpdateContact);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ResponseMessage = $"Kategori Güncellenirken Bir Hata Oluştu! StatusCode: {response.StatusCode}";

            contactViewModel.HttpResponseMessage = response.StatusMessage;
            contactViewModel.ApiResponseMessage = response.ApiResponseMessage;

            return View(contactViewModel);
        }

        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteDataAsync(id);

            return RedirectToAction("Index");
        }
    }
}
