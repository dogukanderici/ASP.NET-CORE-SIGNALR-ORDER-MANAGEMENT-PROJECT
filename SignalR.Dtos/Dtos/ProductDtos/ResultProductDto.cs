using SignalR.Dtos.Dtos.BasketDtos;
using SignalR.Dtos.Dtos.CategoryDtos;
using SignalR.Dtos.Dtos.OrderDetailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.ProductDtos
{
    public class ResultProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool ProductStatus { get; set; }
        public int CategoryID { get; set; }
        public ResultCategoryDto Category { get; set; }
        public List<ResultOrderDetailDto> OrderDetail { get; set; }
        public List<ResultBasketDto> Baskets { get; set; }
    }
}
