using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.RestaurantTableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.RestaurantTableValidations
{
    public class UpdateRestaurantTableValidation : AbstractValidator<UpdateRestaurantTableDto>
    {
        public UpdateRestaurantTableValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage($"Masa Adı {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.PersonCount).NotEmpty().WithMessage($"Masa Kişi Sayısı {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.QRCodeForTable).NotEmpty().WithMessage($"Masa QR Kod {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
