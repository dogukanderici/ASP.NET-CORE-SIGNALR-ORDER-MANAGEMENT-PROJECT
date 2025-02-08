using SignalRWebUI.Areas.Admin.Dtos.FeatureDtos;
using System.Net;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class FeatureViewModel : GenericViewModel
    {
        public List<ResultAdminFeatureDto> ResultFeatures { get; set; }
        public CreateAdminFeatureDto CreateFeature { get; set; }
        public UpdateAdminFeatureDto UpdateFeature { get; set; }
    }
}
