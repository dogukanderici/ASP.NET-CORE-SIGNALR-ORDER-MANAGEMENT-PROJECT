using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalR.Dtos.Dtos.ContactDtos;
using SignalR.Dtos.Dtos.SocialMediaDtos;
using SignalR.Dtos.Dtos.WorkingHourDtos;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract;
using SignalRWebUI.Settings;
using System.Net;

namespace SignalRWebUI.ViewComponents.UILayoutComponents
{
    public class _UILayoutFooterComponentPartial : ViewComponent
    {
        private readonly IContactServicePS _contactService;
        private readonly ISocialMediaServicePS _socialMediaService;
        private readonly IWorkingHourServicePS _workingHourService;

        public _UILayoutFooterComponentPartial(IContactServicePS contactService, ISocialMediaServicePS socialMediaService, IWorkingHourServicePS workingHourService)
        {
            _contactService = contactService;
            _socialMediaService = socialMediaService;
            _workingHourService = workingHourService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var responseContact = await _contactService.GetAllDataAsync(null);
            var responseSocialMedia = await _socialMediaService.GetAllDataAsync(null);
            var responseWorkingHour = await _workingHourService.GetAllDataAsync(null);

            UIFooterViewModel model = new UIFooterViewModel();

            if ((responseContact.StatusMessage != HttpStatusCode.OK) &&
                (responseSocialMedia.StatusMessage != HttpStatusCode.OK) &&
                (responseWorkingHour.StatusMessage != HttpStatusCode.OK))
            {
                model.HttpResponseMessageContact = "Bu Sayfayı Görüntülemek İçin Yetkiniz Bulunmamaktadır!";
                model.HttpResponseMessageSocialMedia = "Bu Sayfayı Görüntülemek İçin Yetkiniz Bulunmamaktadır!";
                model.HttpResponseMessageWorkingHour = "Bu Sayfayı Görüntülemek İçin Yetkiniz Bulunmamaktadır!";
            }

            model.Contacts = responseContact.GetData;
            model.SocialMedias = responseSocialMedia.GetData;
            model.WorkingHours = responseWorkingHour.GetData;

            return View(model);
        }
    }
}
