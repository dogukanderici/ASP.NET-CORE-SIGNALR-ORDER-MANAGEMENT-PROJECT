using AutoMapper;
using SignalR.IdentityServerApi.Dto;
using SignalR.IdentityServerApi.Models;

namespace SignalR.IdentityServerApi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<ApplicationUser, UserLoginDto>().ReverseMap();
            CreateMap<ApplicationUser, UserRegisterDto>().ReverseMap();
        }
    }
}
