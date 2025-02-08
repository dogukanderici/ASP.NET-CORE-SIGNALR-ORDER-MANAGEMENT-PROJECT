using SignalR.Dtos.Dtos.BasketDtos;
using SignalRWebUI.Areas.Admin.Dtos.BasketDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface IBasketServicePS : IDefaultGenericService<ResultBasketDto, List<ResultBasketDto>, CreateAdminBasketDto, UpdateAdminBasketDto>
    {
        Task DeleteBasketItemAsync(int id,int tableID);
    }
}
