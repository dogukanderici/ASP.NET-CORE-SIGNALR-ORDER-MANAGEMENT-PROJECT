using SignalRWebUI.Areas.Admin.Dtos.OrderDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class OrderViewModel : GenericViewModel
    {
        public ResultAdminOrderDto OrderData { get; set; }
    }
}
