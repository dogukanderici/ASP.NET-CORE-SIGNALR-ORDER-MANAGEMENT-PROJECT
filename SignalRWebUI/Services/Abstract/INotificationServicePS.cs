using SignalR.Dtos.Dtos.NotificationDtos;
using SignalRWebUI.Areas.Admin.Dtos.NotificationDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface INotificationServicePS : IDefaultGenericService<ResultNotificationDto, List<ResultNotificationDto>, CreateAdminNotificationDto, UpdateAdminNotificationDto>
    {
        Task<IHttpResponseMessageSettings<ResultNotificationDto>> GetDataWithStatusAsync(int id, bool status);
        Task<IHttpResponseMessageSettings<ResultNotificationDto>> UpdateDataWithStatusAsync(int id, bool status);
    }
}
