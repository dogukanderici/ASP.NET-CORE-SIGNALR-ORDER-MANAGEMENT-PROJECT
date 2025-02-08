using SignalR.Dtos.Dtos.BasketDtos;
using SignalR.Dtos.Dtos.Discounts;
using SignalRWebUI.Areas.Admin.Dtos.BasketDtos;
using SignalRWebUI.Areas.Admin.Models;

namespace SignalRWebUI.Models
{
    public class BasketViewModel : GenericViewModel
    {
        public List<ResultBasketDto> BasketListData { get; set; }
        public ResultBasketDto BasketData { get; set; }
        public CreateAdminBasketDto CreateBasketData { get; set; }
        public UpdateAdminBasketDto UpdateBasketData { get; set; }
        public decimal TotalBasketPrice { get; set; }
        public decimal TotalBasketPriceWithDiscount { get; set; }
        public ResultDiscountDto DiscountData { get; set; }
    }
}
