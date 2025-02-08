using SignalR.Dtos.Dtos.DailyDiscountDtos;

namespace SignalRWebUI.Models
{
    public class DefaultDailyDiscountViewModel
    {
        public List<ResultDailyDiscountDto> ResultDailyDiscounts { get; set; }

        public string HttpResponseMessage { get; set; }
    }
}
