using SignalR.Dtos.Dtos.MoneyCaseDtos;
using SignalRWebUI.Areas.Admin.Dtos.MoneyCaseDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IMoneyCaseServicePS : IDefaultGenericService<ResultMoneyCaseDto, List<ResultMoneyCaseDto>, CreateAdminMoneyCaseDto, UpdateAdminMoneyCaseDto>
    {
        Task<IHttpResponseMessageSettings<List<ResultMoneyCaseDto>>> GetAllOutcomeDataAsync();
    }
}
