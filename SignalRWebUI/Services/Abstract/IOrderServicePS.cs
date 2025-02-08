using SignalR.Dtos.Dtos.OrderDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IOrderServicePS : IDefaultGenericService<ResultOrderDto, List<ResultOrderDto>, CreateAdminOrderDto, UpdateAdminOrderDto>
    {
        Task<IHttpResponseMessageSettings<ResultOrderDto>> GetDataByTableAsync(string table);
    }
}
