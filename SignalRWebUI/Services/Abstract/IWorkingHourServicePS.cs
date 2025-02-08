using SignalR.Dtos.Dtos.WorkingHourDtos;
using SignalRWebUI.Areas.Admin.Dtos.WorkingHourDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface IWorkingHourServicePS : IDefaultGenericService<ResultWorkingHourDto, List<ResultWorkingHourDto>, CreateAdminWorkingHourDto, UpdateAdminWorkingHourDto>
    {
    }
}
