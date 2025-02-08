using AutoMapper;
using SignalR.Dtos.Dtos.MainMessageDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class MainMessageMapping : Profile
    {
        public MainMessageMapping()
        {
            CreateMap<MainMessage, ResultMainMessageDto>().ReverseMap();
            CreateMap<MainMessage, CreateMainMessageDto>().ReverseMap();
            CreateMap<MainMessage, UpdateMainMessageDto>().ReverseMap();
        }
    }
}
