using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.HelpDeskDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.HelpDeskValidations
{
    public class CreateHelpDeskValidation : AbstractValidator<CreateHelpDeskDto>
    {
        public CreateHelpDeskValidation()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage($"Ad / Soyad {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Mail).NotEmpty().WithMessage($"E-Posta {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Phone).NotEmpty().WithMessage($"Telefon {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Subject).NotEmpty().WithMessage($"Konu {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage($"İçerik {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
