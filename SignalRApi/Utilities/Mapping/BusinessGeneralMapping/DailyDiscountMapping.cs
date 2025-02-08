using AutoMapper;
using SignalR.Dtos.Dtos.DailyDiscountDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class DailyDiscountMapping : Profile
    {
        public DailyDiscountMapping()
        {
            CreateMap<DailyDiscount, ResultDailyDiscountDto>().ReverseMap();
            CreateMap<DailyDiscount, CreateDailyDiscountDto>().ReverseMap();
            CreateMap<DailyDiscount, UpdateDailyDiscountDto>().ReverseMap();
        }
    }
}
