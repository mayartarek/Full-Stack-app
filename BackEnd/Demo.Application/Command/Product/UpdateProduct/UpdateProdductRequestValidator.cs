using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command.Product.UpdateProduct
{
    public class UpdateProdductRequestValidator:AbstractValidator<UpdateProdductRequest>    
    {
        public UpdateProdductRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
        }
    }
}
