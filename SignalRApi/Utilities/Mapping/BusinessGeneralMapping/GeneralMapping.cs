using AutoMapper;
using SignalR.Business.Settings;
using SignalR.Dtos.Dtos.PropertySettingsDtos;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<PropertySettings, PropertySettingsDto>().ReverseMap();
        }
    }
}
