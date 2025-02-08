using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.AboutDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.AboutValidations
{
    public class CreateAboutValidation : AbstractValidator<CreateAboutDto>
    {
        public CreateAboutValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage($"Başlık {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Description).NotEmpty().WithMessage($"Açıklama {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage($"Görsel {BusinessValidaitonMessageConstants.NotEmptyMessage}");

            RuleFor(x => x.Title).MinimumLength(5).WithMessage($"Başlık 5 {BusinessValidaitonMessageConstants.MinCharLengthMessage}");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage($"Başlık 50 {BusinessValidaitonMessageConstants.MaxCharLengthMessage}");

            RuleFor(x => x.Description).MinimumLength(50).WithMessage($"Açıklama 50 {BusinessValidaitonMessageConstants.MinCharLengthMessage}");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage($"Açıklama 1000 {BusinessValidaitonMessageConstants.MaxCharLengthMessage}");
        }
    }
}
