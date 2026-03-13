using AutoMapper;
using Demo.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Product.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, GetProductDetailsVm>
    {
        private readonly IMapper mapper;
        private readonly DemoDbContext demoDbContext;

        public GetProductDetailsQueryHandler(IMapper mapper,DemoDbContext demoDbContext)
        {
            this.mapper = mapper;
            this.demoDbContext = demoDbContext;
        }
        public async Task<GetProductDetailsVm> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = await demoDbContext.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            return new GetProductDetailsVm()
            {
                CategoryId = product.CategoryId,
                Description = product.Description,
                DiscountPercentage = product.DiscountPercentage,
                Name = product.Name,
                Id = product.Id,
                Image = product.Image,
                Price = product.Price,
                Stock = product.Stock

            };
         
        }
    }
}
