using SignalR.Dtos.Dtos.DailyDiscountDtos;
using SignalRWebUI.Areas.Admin.Dtos.DailyDiscountDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface IDailyDiscountPS : IDefaultGenericService<ResultDailyDiscountDto, List<ResultDailyDiscountDto>, CreateAdminDailyDiscountDto, UpdateAdminDailyDiscountDto>
    {
    }
}
