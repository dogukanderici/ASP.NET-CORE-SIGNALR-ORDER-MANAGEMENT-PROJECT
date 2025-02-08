using SignalR.Dtos.Dtos.ProductDtos;
using SignalR.Dtos.Dtos.RestaurantTableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.BasketDtos
{
    public class ResultBasketDto
    {
        public int BasketID { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductID { get; set; }
        public int RestaurantTableID { get; set; }

        public ResultProductDto Product { get; set; }
        public ResultRestaurantTableDto RestaurantTable { get; set; }
    }
}
