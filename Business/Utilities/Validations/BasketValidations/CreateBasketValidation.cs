using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.BasketValidations
{
    public class CreateBasketValidation : AbstractValidator<CreateBasketDto>
    {
        public CreateBasketValidation()
        {
            RuleFor(x => x.ProductID).NotEmpty().WithMessage($"Ürün ID {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.RestaurantTableID).NotEmpty().WithMessage($"Masa ID {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Count).NotEmpty().WithMessage($"Ürün Adedi {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Price).NotEmpty().WithMessage($"Ürün Fiyatı {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.TotalPrice).NotEmpty().WithMessage($"Toplam Fiyat {BusinessValidaitonMessageConstants.NotEmptyMessage}");

            RuleFor(x => x.ProductID).GreaterThan(0).WithMessage("Ürün ID Sıfırdan ( 0 ) veya Daha Küçük Olamaz!");
            RuleFor(x => x.RestaurantTableID).GreaterThan(0).WithMessage("Masa ID Sıfırdan ( 0 ) veya Daha Küçük Olamaz!");
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("Ürün Adedi Sıfırdan ( 0 ) veya Daha Küçük Olamaz!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Ürün Fiyatı Sıfırdan ( 0 ) veya Daha Küçük Olamaz!");
            RuleFor(x => x.TotalPrice).GreaterThan(0).WithMessage("Toplam Fiyat Sıfırdan ( 0 ) veya Daha Küçük Olamaz!");
        }
    }
}
