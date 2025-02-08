using SignalR.Dtos.Dtos.Discounts;
using SignalRWebUI.Areas.Admin.Dtos.DiscountDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class DiscountViewModel : GenericViewModel
    {
        public List<ResultDiscountDto> DiscountDataList { get; set; }
        public ResultDiscountDto DiscountData { get; set; }
        public CreateAdminDiscountDto CreateDiscountData { get; set; }
        public UpdateAdminDiscountDto UpdateDiscountData { get; set; }
    }
}
