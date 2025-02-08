﻿using SignalRWebUI.Areas.Admin.Dtos.OrderDetailDtos;

namespace SignalRWebUI.Areas.Admin.Dtos.OrderDtos
{
    public class ResultAdminOrderDto
    {
        public Guid OrderID { get; set; }
        public string Masa { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; } = true;
        public bool IsCanceled { get; set; } = false;
        public string Description { get; set; }

        public List<ResultAdminOrderDetailDto> OrderDetails { get; set; }
    }
}
