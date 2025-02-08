using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.HelpDeskDtos
{
    public class CreateHelpDeskDto
    {
        public Guid HelpDeskID { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
        public Guid ReplyID { get; set; }
    }
}
