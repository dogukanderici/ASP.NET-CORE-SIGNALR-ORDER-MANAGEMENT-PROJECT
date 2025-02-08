using SignalRWebUI.Areas.Admin.Dtos.SendMailDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class SendMailViewModel : GenericViewModel
    {
        public CreateAdminMailDto CreateNewMail { get; set; }
    }
}
