using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.BookingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.BookingValidations
{
    public class CreateBookingValidation : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage($"Ad / Soyad {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Phone).NotEmpty().WithMessage($"Telefon {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Mail).NotEmpty().WithMessage($"E-Posta {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.PersonCount).NotEmpty().WithMessage($"Kişi Sayısı {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Date).NotEmpty().WithMessage($"Rezervasyon Tarihi {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            //RuleFor(x => x.Status).NotEmpty().WithMessage($"Durum {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            //RuleFor(x => x.IsConfirmed).NotEmpty().WithMessage($"Onaylandı Mı? {BusinessValidaitonMessageConstants.NotEmptyMessage}");

            //RuleFor(x => x.Date).LessThan(DateTime.Now).WithMessage("Rezervasyon Tarihi Bugünün Tarihinden Küçük Olamaz!");
        }
    }
}
