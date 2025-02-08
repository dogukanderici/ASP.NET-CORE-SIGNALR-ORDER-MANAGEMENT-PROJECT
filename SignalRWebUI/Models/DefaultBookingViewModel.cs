using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Models;

namespace SignalRWebUI.Models
{
    public class DefaultBookingViewModel : GenericViewModel
    {
        public CreateAdminBookingDto CreateBookingData { get; set; }
    }
}
