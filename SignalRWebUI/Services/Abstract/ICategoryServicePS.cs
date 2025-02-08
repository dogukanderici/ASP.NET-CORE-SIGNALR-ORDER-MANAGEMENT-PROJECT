using SignalR.Dtos.Dtos.CategoryDtos;
using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;

namespace SignalRWebUI.Services.Abstract
{
    public interface ICategoryServicePS : IDefaultGenericService<ResultCategoryDto, List<ResultCategoryDto>, CreateAdminCategoryDto, UpdateAdminCategoryDto>
    {
    }
}
