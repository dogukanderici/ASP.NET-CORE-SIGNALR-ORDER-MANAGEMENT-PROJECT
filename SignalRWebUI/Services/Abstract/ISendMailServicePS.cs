using SignalRWebUI.Areas.Admin.Dtos.SendMailDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface ISendMailServicePS
    {
        Task<IHttpResponseMessageSettings<string>> SendMailAsync(CreateAdminMailDto createAdminMailDto);
    }
}
