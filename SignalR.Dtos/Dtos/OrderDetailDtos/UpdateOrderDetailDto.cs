using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.OrderDetailDtos
{
    public class UpdateOrderDetailDto
    {
        public Guid OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public Guid OrderID { get; set; }
        public int ProductCount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; }
    }
}
