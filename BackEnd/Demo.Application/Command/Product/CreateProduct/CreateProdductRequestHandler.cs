using AutoMapper;
using Demo.Application.CustomException;
using Demo.Infrastructure.Context;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command.Product.CreateProduct
{
    public class CreateProdductRequestHandler : IRequestHandler<CreateProdductRequest,bool>

    {
        private readonly DemoDbContext demoDbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public CreateProdductRequestHandler(DemoDbContext demoDbContext,IMapper mapper, IConfiguration configuration)
        {
            this.demoDbContext = demoDbContext;
            this.mapper = mapper;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(CreateProdductRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateProdductValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var product = new Domain.Entities.Product();
            string imageUrl = null;

            if (request.FormFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(request.FormFile.FileName);

                var path = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await request.FormFile.CopyToAsync(stream);
                }

                imageUrl = this.configuration["FileServer"] +"/images/" + fileName;
            }
            product.Image = imageUrl;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.DiscountPercentage = request.DiscountPercentage;
            product.CategoryId=request.CategoryId;
            await this.demoDbContext.Products.AddAsync(product);

            await this.demoDbContext.SaveChangesAsync();
            return true;
        }
    }
}
