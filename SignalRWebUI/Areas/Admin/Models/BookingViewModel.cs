using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class BookingViewModel : GenericViewModel
    {
        public List<ResultAdminBookingDto> ResultBookings { get; set; }
        public CreateAdminBookingDto CreateBooking { get; set; }
        public UpdateAdminBookingDto UpdateBooking { get; set; }
    }
}
