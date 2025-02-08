﻿namespace SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos
{
    public class CreateAdminRestaurantTableDto
    {
        public string Name { get; set; }
        public int PersonCount { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public string QRCodeForTable { get; set; }
    }
}
