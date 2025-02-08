using SignalR.Dtos.Dtos.ProductDtos;
using SignalRWebUI.Areas.Admin.Dtos.ProductDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IProductServicePS : IDefaultGenericService<ResultProductDto, List<ResultProductDto>, CreateAdminProductDto, UpdateAdminProductDto>
    {
        Task<IHttpResponseMessageSettings<List<ResultProductDto>>> GetAllDataByCategoryAsync(int id);
		Task<IHttpResponseMessageSettings<List<ResultProductDto>>> GetAllDataByCountAsync(int dataCount);
		Task<IHttpResponseMessageSettings<int>> GetProductCount();
    }
}
