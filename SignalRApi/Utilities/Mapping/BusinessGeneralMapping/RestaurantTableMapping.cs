using AutoMapper;
using SignalR.Dtos.Dtos.RestaurantTableDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class RestaurantTableMapping : Profile
    {
        public RestaurantTableMapping()
        {
            CreateMap<RestaurantTable, ResultRestaurantTableDto>().ReverseMap();
            CreateMap<RestaurantTable, CreateRestaurantTableDto>().ReverseMap();
            CreateMap<RestaurantTable, UpdateRestaurantTableDto>().ReverseMap();
        }
    }
}
