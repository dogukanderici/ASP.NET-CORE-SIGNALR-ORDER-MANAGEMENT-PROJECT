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
    public class CreateDailyDiscountValidation : AbstractValidator<CreateDailyDiscountDto>
    {
        public CreateDailyDiscountValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage($"Başlık {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Description).NotEmpty().WithMessage($"Açıklama {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Amount).NotEmpty().WithMessage($"Miktar {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage($"Görsel {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
