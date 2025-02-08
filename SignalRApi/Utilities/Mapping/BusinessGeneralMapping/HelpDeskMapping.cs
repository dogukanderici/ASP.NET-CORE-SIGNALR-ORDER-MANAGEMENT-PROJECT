using AutoMapper;
using SignalR.Dtos.Dtos.HelpDeskDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class HelpDeskMapping : Profile
    {
        public HelpDeskMapping()
        {
            CreateMap<HelpDesk, ResultHelpDeskDto>().ReverseMap();
            CreateMap<HelpDesk, CreateHelpDeskDto>().ReverseMap();
            CreateMap<HelpDesk, UpdateHelpDeskDto>().ReverseMap();
        }
    }
}
