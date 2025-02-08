using SignalRWebUI.Areas.Admin.Dtos.CategoryDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class CategoryViewModel : GenericViewModel
    {
        public List<ResultAdminCategoryDto> ResultCategories { get; set; }
        public CreateAdminCategoryDto CreateCategory { get; set; }
        public UpdateAdminCategoryDto UpdateCategory { get; set; }
    }
}
