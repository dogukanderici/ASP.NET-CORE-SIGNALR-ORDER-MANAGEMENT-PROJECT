namespace SignalRWebUI.Areas.Admin.Dtos.DiscountDtos
{
    public class ResultAdminDiscountDto
    {
        public Guid DiscountID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime LastDay { get; set; }
        public bool IsActive { get; set; }
    }
}
