using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.WorkingHourDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.WorkingHourValidations
{
    public class UpdateWorkingHourValidation : AbstractValidator<UpdateWorkingHourDto>
    {
        public UpdateWorkingHourValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage($"Başlık {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.OpeningHour).NotEmpty().WithMessage($"Açılış Saati {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.ClosingHour).NotEmpty().WithMessage($"Kapanış Saati {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
