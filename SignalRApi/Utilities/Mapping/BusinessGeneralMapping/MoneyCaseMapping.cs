using AutoMapper;
using SignalR.Dtos.Dtos.MoneyCaseDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class MoneyCaseMapping : Profile
    {
        public MoneyCaseMapping()
        {
            CreateMap<MoneyCase, ResultMoneyCaseDto>().ReverseMap();
            CreateMap<MoneyCase, CreateMoneyCaseDto>().ReverseMap();
            CreateMap<MoneyCase, UpdateMoneyCaseDto>().ReverseMap();
        }
    }
}
