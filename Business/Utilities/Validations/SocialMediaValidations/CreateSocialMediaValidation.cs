using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.SocialMediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.SocialMediaValidations
{
    public class CreateSocialMediaValidation : AbstractValidator<CreateSocialMediaDto>
    {
        public CreateSocialMediaValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage($"Başlık {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Url).NotEmpty().WithMessage($"URL {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Icon).NotEmpty().WithMessage($"İkon {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
