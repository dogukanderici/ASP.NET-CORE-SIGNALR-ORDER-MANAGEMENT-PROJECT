using SignalR.Dtos.Dtos.FeatureDtos;
using SignalRWebUI.Areas.Admin.Dtos.FeatureDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface IDefaultFeatureServicePS : IDefaultGenericService<ResultFeatureDto, List<ResultFeatureDto>, CreateAdminFeatureDto, UpdateAdminFeatureDto>
    {
    }
}
