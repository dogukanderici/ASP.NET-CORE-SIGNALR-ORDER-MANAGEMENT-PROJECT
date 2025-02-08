using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.Dtos.Dtos.IdentityDtos;
using SignalR.Entities.Concrete;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract.IdentityServices;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Role")]
    public class RoleController : AdminBaseController
    {
        private readonly IRoleDefinitionServicePS _roleDefinitionServicePS;
        private readonly IMapper _mapper;

        public RoleController(IRoleDefinitionServicePS roleDefinitionServicePS, IMapper mapper)
        {
            _roleDefinitionServicePS = roleDefinitionServicePS;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            RoleDefinitionViewModel model = new RoleDefinitionViewModel();

            var response = await _roleDefinitionServicePS.GetAllRoleListAsync();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = response.StatusMessage;
            }
            else
            {
                model.RoleDefinitionList = response.GetData;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            RoleDefinitionViewModel model = new RoleDefinitionViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(RoleDefinitionViewModel roleDefinitionViewModel)
        {
            var response = await _roleDefinitionServicePS.AddNewRole(roleDefinitionViewModel.CreateRoleDefinition.Name);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            roleDefinitionViewModel.HttpResponseMessage = response.StatusMessage;

            return View(roleDefinitionViewModel);
        }
    }
}
