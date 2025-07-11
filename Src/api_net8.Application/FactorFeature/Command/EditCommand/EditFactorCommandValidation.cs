using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.FactorFeature.Command.EditCommand
{
    public class EditFactorCommandValidation : AbstractValidator<EditFactorCommand>
    {
        public EditFactorCommandValidation()
        {
            RuleFor(x => x.Customer)
                .NotEmpty().WithMessage("نام مشتری اجباری است.")
                .MaximumLength(50).WithMessage("نام مشتری نباید بیشتر از 50 کاراکتر باشد.");

            RuleFor(x => x.DelivaryType)
                .NotEmpty().WithMessage("نوع ارسال اجباری است.")
                .IsInEnum().WithMessage("باید عددی از 1 تا 3 انتخاب شود");

        }
    }
}
