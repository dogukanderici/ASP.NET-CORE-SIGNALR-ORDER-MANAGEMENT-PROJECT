using SignalR.Dtos.Dtos.AboutDtos;
using SignalRWebUI.Areas.Admin.Dtos.AboutDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface IDefaultAboutServicePS : IDefaultGenericService<ResultAboutDto, List<ResultAboutDto>, CreateAdminAboutDto, UpdateAdminAboutDto>
    {
    }
}
