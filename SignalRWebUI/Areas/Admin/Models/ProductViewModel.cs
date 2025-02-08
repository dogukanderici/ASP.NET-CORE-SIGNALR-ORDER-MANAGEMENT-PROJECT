using SignalRWebUI.Areas.Admin.Dtos.ProductDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class ProductViewModel : GenericViewModel
    {
        public List<ResultAdminProductDto> ResultProducts { get; set; }
        public CreateAdminProductDto CreateProduct { get; set; }
        public UpdateAdminProductDto UpdateProduct { get; set; }
    }
}
