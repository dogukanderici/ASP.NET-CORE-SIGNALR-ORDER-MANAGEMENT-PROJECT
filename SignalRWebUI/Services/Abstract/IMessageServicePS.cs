using SignalR.Dtos.Dtos.MessageDtos;
using SignalRWebUI.Areas.Admin.Dtos.MessageDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IMessageServicePS : IDefaultGenericService<ResultMessageDto, List<ResultMessageDto>, CreateAdminMessageDto, UpdateAdminMessageDto>
    {
        Task<IHttpResponseMessageSettings<ResultMessageDto>> GetDataByGuid(Guid id);
        Task<IHttpResponseMessageSettings<List<ResultMessageDto>>> GetAllMainMessageAsync(Guid mainId);
        Task<IHttpResponseMessageSettings<List<ResultMessageDto>>> GetAllInboxMessageAsync(string receiverMail);
        Task<IHttpResponseMessageSettings<List<ResultMessageDto>>> GetAllOutboxMessageAsync(string senderMail);
    }
}
