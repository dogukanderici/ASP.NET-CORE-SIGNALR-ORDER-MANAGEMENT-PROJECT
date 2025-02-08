using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.MessageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.MessageValidations
{
    public class UpdateMessageValidation : AbstractValidator<UpdateMessageDto>
    {
        public UpdateMessageValidation()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage($"Alıcı Mail {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.MessageSubject).NotEmpty().WithMessage($"Konu {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage($"İçerik {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
