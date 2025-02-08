using SignalR.Dtos.Dtos.ContactDtos;
using SignalRWebUI.Areas.Admin.Dtos.ContactDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface IContactServicePS : IDefaultGenericService<ResultContactDto, List<ResultContactDto>, CreateAdminContactDto, UpdateAdminContactDto>
    {
    }
}
