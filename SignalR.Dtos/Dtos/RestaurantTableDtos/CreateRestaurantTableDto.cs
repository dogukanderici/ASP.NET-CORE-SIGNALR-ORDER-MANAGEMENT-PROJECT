using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.RestaurantTableDtos
{
    public class CreateRestaurantTableDto
    {
        public string Name { get; set; }
        public int PersonCount { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public string QRCodeForTable { get; set; }
    }
}
