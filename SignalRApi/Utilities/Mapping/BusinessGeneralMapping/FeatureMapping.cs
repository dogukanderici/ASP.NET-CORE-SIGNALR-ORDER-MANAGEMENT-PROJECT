using AutoMapper;
using SignalR.Dtos.Dtos.FeatureDtos;
using SignalR.Entities.Concrete;

namespace SignalRApi.Utilities.Mapping.BusinessGeneralMapping
{
    public class FeatureMapping : Profile
    {
        public FeatureMapping()
        {
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
        }
    }
}
