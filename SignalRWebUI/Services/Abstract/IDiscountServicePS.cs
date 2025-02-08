using SignalR.Dtos.Dtos.Discounts;
using SignalRWebUI.Areas.Admin.Dtos.DiscountDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IDiscountServicePS : IDefaultGenericService<ResultDiscountDto, List<ResultDiscountDto>, CreateAdminDiscountDto, UpdateAdminDiscountDto>
    {
        Task<IHttpResponseMessageSettings<ResultDiscountDto>> GetDataByGuidAsync(Guid id);
        Task<IHttpResponseMessageSettings<ResultDiscountDto>> GetDataByTitleAsync(string title);
        Task DeleteDataByGuidAsync(Guid id);
    }
}
