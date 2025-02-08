using SignalR.Dtos.Dtos.OrderDetailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public Guid OrderID { get; set; }
        public string Masa { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; } = true;
        public bool IsCanceled { get; set; } = false;
        public string Description { get; set; }
    }
}
