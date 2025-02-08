using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.Discounts
{
    public class CreateDiscountDto
    {
        public Guid DiscountID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime LastDay { get; set; }
        public bool IsActive { get; set; }
    }
}
