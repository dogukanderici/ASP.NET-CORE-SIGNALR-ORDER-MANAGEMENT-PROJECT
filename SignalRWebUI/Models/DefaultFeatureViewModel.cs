using SignalR.Dtos.Dtos.AboutDtos;
using SignalR.Dtos.Dtos.FeatureDtos;

namespace SignalRWebUI.Models
{
    public class DefaultFeatureViewModel
    {
        public List<ResultFeatureDto> ResultFeatures { get; set; }

        public string HttpResponseMessage { get; set; }
    }
}
