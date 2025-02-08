using SignalRWebUI.Areas.Admin.Dtos.DailyDiscountDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class DailyDiscountViewModel : GenericViewModel
    {
        public List<ResultAdminDailyDiscountDto> ResultDailyDiscounts { get; set; }
        public CreateAdminDailyDiscountDto CreateDailyDiscount { get; set; }
        public UpdateAdminDailyDiscountDto UpdateDailyDiscount { get; set; }
    }
}
