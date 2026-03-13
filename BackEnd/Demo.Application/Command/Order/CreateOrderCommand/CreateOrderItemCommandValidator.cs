using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command.Order.CreateOrderCommand
{
    public class CreateOrderItemCommandValidator:AbstractValidator<CreateOrderitemVm>  
    {
        public CreateOrderItemCommandValidator()
        {
            RuleSet("CreateOrderItemCommand", () =>
            {
                RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
                RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
            });
        }
    }
}
