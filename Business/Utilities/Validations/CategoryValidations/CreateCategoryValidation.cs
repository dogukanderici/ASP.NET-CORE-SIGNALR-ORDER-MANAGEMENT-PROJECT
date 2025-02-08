using FluentValidation;
using SignalR.Business.Utilities.Constants;
using SignalR.Dtos.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.Validations.CategoryValidations
{
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage($"Kategori Adı {BusinessValidaitonMessageConstants.NotEmptyMessage}");
            RuleFor(x => x.CategoryName).MinimumLength(5).WithMessage($"Kategori Adı Alanı 5 {BusinessValidaitonMessageConstants.MinCharLengthMessage}");
            RuleFor(x => x.CategoryName).MaximumLength(25).WithMessage($"Kategori Adı Alanı 25 {BusinessValidaitonMessageConstants.MaxCharLengthMessage}");
        }
    }
}
