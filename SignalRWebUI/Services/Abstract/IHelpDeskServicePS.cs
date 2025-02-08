using SignalR.Dtos.Dtos.HelpDeskDtos;
using SignalRWebUI.Areas.Admin.Dtos.HelpDeskDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IHelpDeskServicePS : IDefaultGenericService<ResultHelpDeskDto, List<ResultHelpDeskDto>, CreateAdminHelpDeskDto, UpdateAdminHelpDeskDto>
    {
        Task<IHttpResponseMessageSettings<ResultHelpDeskDto>> GetDataByGuidAsync(Guid id);
        Task<IHttpResponseMessageSettings<List<ResultHelpDeskDto>>> GetDataAdminOutboxByGuidAsync(Guid id);
        Task<IHttpResponseMessageSettings<List<ResultHelpDeskDto>>> GetDataUserOutboxByGuidAsync(string userMail);
        Task<IHttpResponseMessageSettings<List<ResultHelpDeskDto>>> GetDataUserInboxByGuidAsync(string userMail, Guid replyId);
        Task DeleteDataByGuidAsync(Guid id);
    }
}
