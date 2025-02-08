using AutoMapper;
using SignalR.Dtos.Dtos.BasketDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<Basket, ResultBasketDto>().ReverseMap();
            CreateMap<Basket, CreateBasketDto>().ReverseMap();
            CreateMap<Basket, UpdateBasketDto>().ReverseMap();
        }
    }
}
