using AutoMapper;
using SignalR.Dtos.Dtos.WorkingHourDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class WorkingHourMapping : Profile
    {
        public WorkingHourMapping()
        {
            CreateMap<WorkingHour, ResultWorkingHourDto>().ReverseMap();
            CreateMap<WorkingHour, CreateWorkingHourDto>().ReverseMap();
            CreateMap<WorkingHour, UpdateWorkingHourDto>().ReverseMap();
        }
    }
}
