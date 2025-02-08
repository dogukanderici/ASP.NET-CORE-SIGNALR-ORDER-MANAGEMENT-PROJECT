using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Entities.Concrete
{
    public class RestaurantTable
    {
        public int RestaurantTableID { get; set; }
        public string Name { get; set; }
        public int PersonCount { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public string QRCodeForTable { get; set; }
        public List<Basket> Baskets { get; set; }
    }
}
