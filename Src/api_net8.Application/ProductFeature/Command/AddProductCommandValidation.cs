using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.api_net8.Application.ProductFeature.Command
{
    public class AddProductCommandValidation : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidation()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("نام محصول اجباری است.")
                .MaximumLength(50).WithMessage("نام محصول نباید بیشتر از 50 کاراکتر باشد.");

            RuleFor(x => x.ChangeDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("تاریخ فاکتور نمی‌تواند در آینده باشد.");

        }
    }
}
