using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.ContactDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.ContactValidations
{
    public class CreateContactValidation : AbstractValidator<CreateContactDto>
    {
        public CreateContactValidation()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage($"Açıklama {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Mail).NotEmpty().WithMessage($"E-Posta {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Phone).NotEmpty().WithMessage($"Telefon {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Location).NotEmpty().WithMessage($"Konum {BusinessValidaitonMessageConstants.NotEmptyMessage}");

            RuleFor(x => x.Description).MinimumLength(50).WithMessage($"Açıklama Alanı {BusinessValidaitonMessageConstants.MinCharLengthMessage}");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage($"Açıklama Alanı {BusinessValidaitonMessageConstants.MinCharLengthMessage}");

            RuleFor(x => x.Mail).EmailAddress().WithMessage($"{BusinessValidaitonMessageConstants.NotValidEmailMessage}");

        }
    }
}
