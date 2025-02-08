namespace SignalRWebUI.Areas.Admin.Dtos.BasketDtos
{
    public class CreateAdminBasketDto
    {
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductID { get; set; }
        public int RestaurantTableID { get; set; }
    }
}
