﻿using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.MainMessageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.MainMessageValidations
{
    public class CreateMainMessageValidation : AbstractValidator<CreateMainMessageDto>
    {
        public CreateMainMessageValidation()
        {
            RuleFor(x => x.MainMessageSubject).NotEmpty().WithMessage($"Konu {BusinessValidaitonMessageConstants.NotEmptyMessage}");
        }
    }
}
