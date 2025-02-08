using SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class HelpDeskViewModel : GenericViewModel
    {
        public List<ResultAdminHelpDeskDto> HelpDeskList { get; set; }
        public ResultAdminHelpDeskDto HelpDeskData { get; set; }
        public CreateAdminHelpDeskDto CreateHelpDesk { get; set; }
        public UpdateAdminHelpDeskDto UpdateHelpDesk { get; set; }
        public CreateAdminHelpDeskDto ReplyHelpDesk { get; set; }
    }
}
