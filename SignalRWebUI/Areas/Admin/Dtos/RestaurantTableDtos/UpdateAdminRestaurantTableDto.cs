namespace SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos
{
    public class UpdateAdminRestaurantTableDto
    {
        public int RestaurantTableID { get; set; }
        public string Name { get; set; }
        public int PersonCount { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public string QRCodeForTable { get; set; }
    }
}
