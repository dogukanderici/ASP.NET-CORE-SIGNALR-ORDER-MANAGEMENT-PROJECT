using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Entities.Concrete
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public Guid OrderID { get; set; }
        public int ProductCount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
