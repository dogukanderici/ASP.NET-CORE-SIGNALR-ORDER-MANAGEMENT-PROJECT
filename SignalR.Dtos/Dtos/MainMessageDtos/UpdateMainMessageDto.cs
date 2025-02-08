using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.MainMessageDtos
{
    public class UpdateMainMessageDto
    {
        public int MainMessageID { get; set; }
        public string MainMessageSubject { get; set; }
        public int MessageID { get; set; }
    }
}
