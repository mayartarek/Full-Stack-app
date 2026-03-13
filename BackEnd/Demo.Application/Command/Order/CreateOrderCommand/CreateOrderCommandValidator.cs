using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command.Order.CreateOrderCommand
{
    public class CreateOrderCommandValidator:AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
           RuleSet("CreateOrderCommand", () =>
           {
               RuleFor(x => x.CreateUserDetails).NotEmpty().WithMessage("CustomerId is required");
               RuleFor(x => x.OrderItem).NotEmpty().WithMessage("OrderItems is required");
               RuleForEach(x => x.OrderItem).SetValidator(new CreateOrderItemCommandValidator());
           });
        }
    }
}
