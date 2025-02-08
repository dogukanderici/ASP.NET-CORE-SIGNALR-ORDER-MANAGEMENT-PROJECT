using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.BookingDtos
{
    public class CreateBookingDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public int PersonCount { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
