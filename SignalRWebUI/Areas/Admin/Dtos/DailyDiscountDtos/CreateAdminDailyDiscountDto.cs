namespace SignalRWebUI.Areas.Admin.Dtos.DailyDiscountDtos
{
    public class CreateAdminDailyDiscountDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
