using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.WorkingHourDtos
{
    public class UpdateWorkingHourDto
    {
        public int WorkingHourID { get; set; }
        public string Title { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public bool Status { get; set; }
    }
}
