using SignalR.Dtos.Dtos.OrderDetailDtos;
using SignalRWebUI.Areas.Admin.Dtos.OrderDetailDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IOrderDetailServicePS : IDefaultGenericService<ResultOrderDetailDto, List<ResultOrderDetailDto>, CreateAdminOrderDetailDto, UpdateAdminOrderDetailDto>
    {
        Task<IHttpResponseMessageSettings<List<ResultOrderDetailDto>>> GetAllDataByOrderIdAsync(Guid id);
    }
}