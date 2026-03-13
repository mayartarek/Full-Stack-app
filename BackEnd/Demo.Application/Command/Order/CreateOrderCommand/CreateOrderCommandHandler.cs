using Demo.Application.Command.Product.CreateProduct;
using Demo.Application.Constract.Interface;
using Demo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Demo.Application.Command.Order.CreateOrderCommand
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggedInerface loggedInerface;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork,ILoggedInerface loggedInerface)
        {
            this.unitOfWork = unitOfWork;
            this.loggedInerface = loggedInerface;
        }
        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new CustomException.ValidationException(validationResult);
            var order = new Domain.Entities.Order
            {
                CreatedAt = DateTime.Now,
                ShippingAddress = request.CreateUserDetails.ShippingAddress,
                Email = request.CreateUserDetails.Email,
                PhoneNumber= request.CreateUserDetails.PhoneNumber,
                CreatedBy = this.loggedInerface.UserId,

            };



            foreach(var item in request.OrderItem)
            {
                var product = await this.unitOfWork.Products.GetByIdAsync(item.ProductId);  
                if(product== null)
                    throw new CustomException.NotFoundException($"Produc",item.ProductId);
                var productAvailabele = await this.unitOfWork.CheckAvailabilty(item.ProductId,item.Quantity);
                if(productAvailabele == false)
                    throw new CustomException.BadRequestException($"Product with id {item.ProductId} not found or not enough quantity available.");
                var orderItem = new OrderItem
                {
                    Quantity = item.Quantity,
                    OrderId = order.Id,
                    ProductDetailsJson= JsonSerializer.Serialize(product),
                };
                order.TotalPrice=order.TotalPrice+ (product.Price * item.Quantity);
                order.OrderItems.Add(orderItem); 
            }

            

            await this.unitOfWork.Orders.AddAsync(order);

            await this.unitOfWork.SaveChangesAsync();


            return true;
        }
    }
}
