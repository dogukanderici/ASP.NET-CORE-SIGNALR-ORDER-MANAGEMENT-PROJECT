using SignalRWebUI.Areas.Admin.Dtos.ContactDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class ContactViewModel : GenericViewModel
    {
        public List<ResultAdminContactDto> ResultContacts { get; set; }
        public CreateAdminContactDto CreateContact { get; set; }
        public UpdateAdminContactDto UpdateContact { get; set; }
    }
}
