namespace SignalRWebUI.Areas.Admin.Dtos.ProductDtos
{
    public class UpdateAdminProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public bool ProductStatus { get; set; }
        public int CategoryID { get; set; }
    }
}
