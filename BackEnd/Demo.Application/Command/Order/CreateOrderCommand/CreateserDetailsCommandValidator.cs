using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command.Order.CreateOrderCommand
{
    public class CreateserDetailsCommandValidator:AbstractValidator<CreateUserDetailsVm>
    {
        public CreateserDetailsCommandValidator()
        {
            RuleFor(a=>a.Email).NotEmpty().WithMessage("Email Is Required");
            RuleFor(a => a.PhoneNumber).NotEmpty().WithMessage("Phone Is Required");

            RuleFor(a => a.ShippingAddress).NotEmpty().WithMessage("Address Is Required");

        }
    }
}
