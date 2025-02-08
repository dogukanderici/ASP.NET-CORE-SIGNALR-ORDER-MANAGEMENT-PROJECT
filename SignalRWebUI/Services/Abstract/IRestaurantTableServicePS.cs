using SignalR.Dtos.Dtos.RestaurantTableDtos;
using SignalRWebUI.Areas.Admin.Dtos.RestaurantTableDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IRestaurantTableServicePS : IDefaultGenericService<ResultRestaurantTableDto, List<ResultRestaurantTableDto>, CreateAdminRestaurantTableDto, UpdateAdminRestaurantTableDto>
    {
        Task<IHttpResponseMessageSettings<ResultRestaurantTableDto>> GetTableIdByTableName(string tableName);
    }
}
