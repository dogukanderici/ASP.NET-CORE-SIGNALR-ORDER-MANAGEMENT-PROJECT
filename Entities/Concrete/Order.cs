using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Entities.Concrete
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }
        public string Masa { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; } = true;
        public bool IsCanceled { get; set; } = false;
        public string Description { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
