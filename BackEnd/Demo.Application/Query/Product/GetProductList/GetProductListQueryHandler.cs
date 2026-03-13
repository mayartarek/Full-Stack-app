using AutoMapper;
using Demo.Application.Helper;
using Demo.Domain.Entities;
using Demo.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Product.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, PaginationList<GetProductListVm>>
    {
        private readonly IMapper mapper;
        private readonly DemoDbContext demoDbContext;

        public GetProductListQueryHandler(IMapper mapper, DemoDbContext demoDbContext)
        {
            this.mapper = mapper;
            this.demoDbContext = demoDbContext;
        }
        public async Task<PaginationList<GetProductListVm>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var PRODUCTS =await GetProducts(request); 
            var result = new PaginationList<GetProductListVm>
            {
                List = PRODUCTS,
                Page = request.PageNumber,
                Size = request.PageSize,
                Count = await demoDbContext.Products.CountAsync()
            };
            return result;
        }
        public async Task<List<GetProductListVm>> GetProducts(GetProductListQuery request)
        {
            var query = demoDbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.Filter.Name))
            {
                var nameFilter = request.Filter.Name.ToLower();
                query = query.Where(a => a.Name.ToLower().Contains(request.Filter.Name.ToLower()));
            }

            if (request.Filter.CategoryIds != null && request.Filter.CategoryIds.Count > 0)
            {
                query = query.Where(a => request.Filter.CategoryIds.Contains(a.CategoryId));
            }

            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(a => new GetProductListVm
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    DiscountPercentage = a.DiscountPercentage,
                    Description = a.Description,
                    Image = a.Image,
                    Stock = a.Stock,
                })
                .ToListAsync();

            return products;
        }
    }
}
