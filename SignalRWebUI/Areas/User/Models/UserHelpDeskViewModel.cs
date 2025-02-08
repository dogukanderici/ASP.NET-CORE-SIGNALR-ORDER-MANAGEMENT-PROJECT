using SignalR.Dtos.Dtos.HelpDeskDtos;
using SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos;
using SignalRWebUI.Areas.Admin.Models;

namespace SignalRWebUI.Areas.User.Models
{
    public class UserHelpDeskViewModel : GenericViewModel
    {
        public List<ResultHelpDeskDto> HelpDeskListData { get; set; }
        public ResultHelpDeskDto HelpDeskData { get; set; }
        public CreateAdminHelpDeskDto CreateHelpDesk { get; set; }
    }
}
