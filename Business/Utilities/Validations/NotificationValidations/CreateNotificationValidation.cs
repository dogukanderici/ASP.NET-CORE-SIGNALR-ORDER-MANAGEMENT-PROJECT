using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.NotificationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.NotificationValidations
{
    public class CreateNotificationValidation : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationValidation()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage($"Tip {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.Date).NotEmpty().WithMessage($"Tarih {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
