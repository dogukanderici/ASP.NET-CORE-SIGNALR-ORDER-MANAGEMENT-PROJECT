using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.DailyDiscountDtos
{
    public class ResultDailyDiscountDto
    {
        public int DailyDiscountID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string ImageUrl { get; set; }
    }
}
