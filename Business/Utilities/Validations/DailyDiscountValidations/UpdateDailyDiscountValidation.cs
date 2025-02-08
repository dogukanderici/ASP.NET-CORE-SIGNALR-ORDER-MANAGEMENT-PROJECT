using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.DailyDiscountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.DailyDiscountValidations
{
    public class UpdateDailyDiscountValidation : AbstractValidator<UpdateDailyDiscountDto>
    {
        public UpdateDailyDiscountValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage($"Başlık {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Description).NotEmpty().WithMessage($"Açıklama {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Amount).NotEmpty().WithMessage($"Miktar {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage($"Görsel {BusinessValidaitonMessageConstants.NotEmptyMessage}");

            RuleFor(x => x.Amount).LessThanOrEqualTo(0).WithMessage("Miktar 0 Veya Daha Düşük Olamaz!");
        }
    }
}
