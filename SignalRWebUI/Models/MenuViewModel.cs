using SignalR.Dtos.Dtos.CategoryDtos;
using SignalR.Dtos.Dtos.ProductDtos;
using SignalRWebUI.Areas.Admin.Models;

namespace SignalRWebUI.Models
{
    public class MenuViewModel : GenericViewModel
    {
        public List<ResultProductDto> ProductList { get; set; }
        public List<ResultCategoryDto> CategoryList { get; set; }
    }
}
