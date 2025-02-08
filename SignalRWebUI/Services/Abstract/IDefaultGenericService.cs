using SignalRWebUI.Areas.Admin.Dtos.AboutDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IDefaultGenericService<TResult, TResulList, TCreate, TUpdate>
    {
        Task<IHttpResponseMessageSettings<TResulList>> GetAllDataAsync(string? endPoint);
        Task<IHttpResponseMessageSettings<TResult>> GetDataAsync(int id);
        Task<IHttpResponseMessageSettings<TCreate>> CreateDataAsync(TCreate createMdoel);
        Task<IHttpResponseMessageSettings<TUpdate>> UpdateDataAsync(TUpdate updateModel);

        Task DeleteDataAsync(int id);
    }
}
