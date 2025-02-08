using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.TestimonialDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.TestimonialValidations
{
    public class CreateTestimonialValidation : AbstractValidator<CreateTestimonialDto>
    {
        public CreateTestimonialValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage($"Ad / Soyad {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Title).NotEmpty().WithMessage($"Başlık {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Comment).NotEmpty().WithMessage($"Yorum {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage($"Görsel {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
