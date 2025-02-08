using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class RestaurantTableViewModel : GenericViewModel
    {
        public List<ResultAdminRestaurantTableDto> ResultRestaurants { get; set; }
        public CreateAdminRestaurantTableDto CreateRestaurantTable { get; set; }
        public UpdateAdminRestaurantTableDto UpdateRestaurant { get; set; }
        public bool CreateNewQRCodeForUpdate { get; set; } = false;
    }
}
