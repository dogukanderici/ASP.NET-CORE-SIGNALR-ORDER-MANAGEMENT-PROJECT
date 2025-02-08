using SignalR.Dtos.Dtos.SocialMediaDtos;
using SignalRWebUI.Areas.Admin.Dtos.SocialMediaDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class SocialMediaViewModel : GenericViewModel
    {
        public List<ResultSocialMediaDto> ResultSocialMedias { get; set; }
        public CreateAdminSocialMediaDto CreateSocialMedia { get; set; }
        public UpdateAdminSocialMediaDto UpdateSocialMedia { get; set; }
    }
}
