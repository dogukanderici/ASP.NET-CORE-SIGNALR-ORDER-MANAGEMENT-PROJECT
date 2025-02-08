namespace SignalRWebUI.Areas.Admin.Dtos.BookingDtos
{
    public class ResultAdminBookingDto
    {
        public int BookingID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public int PersonCount { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
