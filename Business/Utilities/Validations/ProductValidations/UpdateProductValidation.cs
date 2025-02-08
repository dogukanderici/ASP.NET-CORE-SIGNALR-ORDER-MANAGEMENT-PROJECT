using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.ProductValidations
{
    public class UpdateProductValidation : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidation()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage($"Ürün Adı {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Description).NotEmpty().WithMessage($"Açıklama {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Price).NotEmpty().WithMessage($"Ürün Fiyatı {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage($"Ürün Görseli {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage($"Kategori {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
