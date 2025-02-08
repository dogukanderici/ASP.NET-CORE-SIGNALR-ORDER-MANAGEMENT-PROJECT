using SignalRWebUI.Areas.Admin.Dtos.MessageDtos;
using SignalRWebUI.Areas.Admin.Models;

namespace SignalRWebUI.Areas.User.Models
{
    public class MessageViewModel : GenericViewModel
    {
        public List<ResultAdminMessageDto> ResultInboxMessages { get; set; }
        public List<ResultAdminMessageDto> ResultOutboxMessages { get; set; }
        public ResultAdminMessageDto MessageData { get; set; }
        public CreateAdminMessageDto CreateMessage { get; set; }
        public UpdateAdminMessageDto UpdateMessage { get; set; }
    }
}
