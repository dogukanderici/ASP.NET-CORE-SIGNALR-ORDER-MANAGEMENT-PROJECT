using SignalR.Dtos.Dtos.OrderDetailDtos;
using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;

namespace SignalRWebUI.Areas.Admin.Dtos.ProductDtos
{
    public class ResultAdminProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool ProductStatus { get; set; }
        public int CategoryID { get; set; }
        public ResultAdminCategoryDto Category { get; set; }
        public List<ResultOrderDetailDto> OrderDetail { get; set; }
    }
}
