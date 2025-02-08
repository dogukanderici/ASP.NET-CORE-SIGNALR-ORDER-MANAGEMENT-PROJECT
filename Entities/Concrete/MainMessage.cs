using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Entities.Concrete
{
    public class MainMessage
    {
        [Key]
        public int MainMessageID { get; set; }
        public string MainMessageSubject { get; set; }
        public int MessageID { get; set; }
        public List<Message> Messages { get; set; }
    }
}
