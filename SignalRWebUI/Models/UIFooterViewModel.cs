using SignalR.Dtos.Dtos.ContactDtos;
using SignalR.Dtos.Dtos.SocialMediaDtos;
using SignalR.Dtos.Dtos.WorkingHourDtos;

namespace SignalRWebUI.Models
{
    public class UIFooterViewModel
    {
        public List<ResultContactDto> Contacts { get; set; }
        public List<ResultSocialMediaDto> SocialMedias { get; set; }
        public List<ResultWorkingHourDto> WorkingHours { get; set; }
        public string HttpResponseMessageContact { get; set; }
        public string HttpResponseMessageSocialMedia { get; set; }
        public string HttpResponseMessageWorkingHour { get; set; }
    }
}
