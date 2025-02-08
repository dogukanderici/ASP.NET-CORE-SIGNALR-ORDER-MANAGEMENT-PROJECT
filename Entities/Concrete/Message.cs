using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Entities.Concrete
{
    public class Message
    {
        [Key]
        public Guid MessageID { get; set; }
        public Guid MainMessageID { get; set; }
        public string SenderMail { get; set; }
        public string ReceiverMail { get; set; }
        public string MessageSubject { get; set; }
        public string MessageContent { get; set; }
        public string MessageType { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
