using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Entities.Concrete
{
    public class WorkingHour
    {
        [Key]
        public int WorkingHourID { get; set; }
        public string Title { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public bool Status { get; set; }
    }
}
