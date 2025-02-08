﻿using SignalR.Dtos.Dtos.OrderDtos;
using SignalR.Dtos.Dtos.ProductDtos;

namespace SignalRWebUI.Areas.Admin.Dtos.OrderDetailDtos
{
    public class CreateAdminOrderDetailDto
    {
        public Guid OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public Guid OrderID { get; set; }
        public int ProductCount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; }
    }
}
