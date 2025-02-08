using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.MainMessageDtos
{
    public class CreateMainMessageDto
    {
        public string MainMessageSubject { get; set; }
        public int MessageID { get; set; }
    }
}
