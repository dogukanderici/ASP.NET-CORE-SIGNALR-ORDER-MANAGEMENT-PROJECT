using SignalR.Dtos.Dtos.MoneyCaseDtos;
using SignalRWebUI.Areas.Admin.Dtos.MoneyCaseDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class CostManagementViewModel : GenericViewModel
    {
        public List<ResultAdminMoneyCaseDto> ResultMoneyCases { get; set; }
        public ResultAdminMoneyCaseDto ResultMoneyCase { get; set; }
        public CreateAdminMoneyCaseDto CreateMoneyCase { get; set; }
        public UpdateAdminMoneyCaseDto UpdateMoneyCase { get; set; }
    }
}
