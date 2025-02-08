using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.MoneyCaseDtos
{
    public class ResultMoneyCaseDto
    {
        public int MoneyCaseID { get; set; }
        public decimal TotalAmount { get; set; }
        public string OperationType { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
