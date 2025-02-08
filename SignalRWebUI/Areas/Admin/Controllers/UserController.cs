using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.Dtos.Dtos.IdentityDtos;
using SignalR.Entities.Concrete;
using SignalRWebUI.Areas.Admin.Models;
using SignalRWebUI.Services.Abstract.IdentityServices;
using System.Collections.Generic;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/User")]
    public class UserController : AdminBaseController
    {
        private readonly IUserRoleDefinitonServicePS _userRoleDefinitionService;
        private readonly IRoleDefinitionServicePS _roleDefinitionService;
        private readonly IMapper _mapper;

        public UserController(IUserRoleDefinitonServicePS userRoleDefinitionService, IRoleDefinitionServicePS roleDefinitionService, IMapper mapper)
        {
            _userRoleDefinitionService = userRoleDefinitionService;
            _roleDefinitionService = roleDefinitionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _userRoleDefinitionService.GetAllUserAsync();

            UserRoleDefinitionViewModel model = new UserRoleDefinitionViewModel();

            if (response.StatusMessage != HttpStatusCode.OK)
            {
                model.HttpResponseMessage = response.StatusMessage;
            }
            else
            {
                model.UserDataList = response.GetData;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Create/{id}/{roleName}")]
        public async Task<IActionResult> AddNewRoleToUser(string id, string roleName)
        {

            var response = await _userRoleDefinitionService.CreateNewUserRole(id, roleName);

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                return RedirectToAction("Update", new { id = id });
            }

            ViewBag.Info = "Kullanıcının Rol Tanımı Bulunmmaktadır!";

            return RedirectToAction("Update", new { id = id });
        }

        [HttpGet]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(string id)
        {
            var response = await _userRoleDefinitionService.GetUserAsync(id);
            var responseUserData = await _userRoleDefinitionService.GetUserDataAsync(id);
            var responseRoleList = await _roleDefinitionService.GetAllRoleListAsync();

            UserRoleDefinitionViewModel model = new UserRoleDefinitionViewModel();

            if (response.StatusMessage == HttpStatusCode.OK)
            {
                if (responseUserData.StatusMessage == HttpStatusCode.OK)
                {
                    if (responseRoleList.StatusMessage == HttpStatusCode.OK)
                    {
                        ViewBag.RoleList = responseRoleList.GetData;
                    }
                    else
                    {
                        ViewBag.RoleList = new List<string>();
                    }

                    model.UserRoleData = response.GetData;

                    ViewBag.UserName = responseUserData.GetData.UserName;
                    ViewBag.UserId = responseUserData.GetData.Id;
                }
                else
                {
                    model.HttpResponseMessage = response.StatusMessage;
                }
            }
            else
            {
                model.HttpResponseMessage = response.StatusMessage;
            }

            return View(model);
        }

        [HttpGet]
        [Route("Delete/{id}/{roleName}")]
        public async Task<IActionResult> DeleteUserRole(string id, string roleName)
        {
            var response = await _userRoleDefinitionService.DeleteUserRole(id, roleName);

            if (response.StatusMessage == HttpStatusCode.OK)
            {

                return RedirectToAction("Update", new { id = id });
            }

            ViewBag.Info = "Kullanıcının Rol Tanımı Bulunmmaktadır!";

            return RedirectToAction("Update", new { id = id });
        }
    }
}
