using SignalR.Dtos.Dtos.SocialMediaDtos;
using SignalRWebUI.Areas.Admin.Dtos.SocialMediaDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface ISocialMediaServicePS : IDefaultGenericService<ResultSocialMediaDto, List<ResultSocialMediaDto>, CreateAdminSocialMediaDto, UpdateAdminSocialMediaDto>
    {
    }
}
