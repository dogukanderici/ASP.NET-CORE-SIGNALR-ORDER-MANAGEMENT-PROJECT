using SignalRWebUI.Areas.Admin.Dtos.AboutDtos;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class AboutViewModel : GenericViewModel
    {
        public List<ResultAdminAboutDto> ResultAbouts { get; set; }
        public CreateAdminAboutDto CreateAbout { get; set; }
        public UpdateAdminAboutDto UpdateAbout { get; set; }
    }
}
