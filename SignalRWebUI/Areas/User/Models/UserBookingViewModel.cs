using SignalR.Dtos.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Models;

namespace SignalRWebUI.Areas.User.Models
{
    public class UserBookingViewModel : GenericViewModel
    {
        public List<ResultBookingDto> BookingListData { get; set; }
        public ResultBookingDto BookingData { get; set; }
        public CreateAdminBookingDto CreateBookingData { get; set; }
    }
}
