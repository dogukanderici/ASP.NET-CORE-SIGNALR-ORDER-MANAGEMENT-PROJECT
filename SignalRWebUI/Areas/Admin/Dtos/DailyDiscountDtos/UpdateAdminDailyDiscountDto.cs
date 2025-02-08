namespace SignalRWebUI.Areas.Admin.Dtos.DailyDiscountDtos
{
    public class UpdateAdminDailyDiscountDto
    {
        public int DailyDiscountID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
