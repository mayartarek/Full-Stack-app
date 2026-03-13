using AutoMapper;
using Demo.Application.Constract.Interface;
using MediatR;
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
        private readonly IRepositoryPattern<Domain.Entities.Product> demoDbContext;

        public GetProductDetailsQueryHandler(IMapper mapper, IRepositoryPattern<Domain.Entities.Product> demoDbContext)
        {
            this.mapper = mapper;
            this.demoDbContext = demoDbContext;
        }
        public async Task<GetProductDetailsVm> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = await demoDbContext.GetByIdAsync( request.Id );
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
