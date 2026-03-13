using AutoMapper;
using Demo.Application.Command.Product.CreateProduct;
using Demo.Application.Command.Product.UpdateProduct;
using Demo.Application.Query.Product.GetProductDetails;
using Demo.Application.Query.Product.GetProductList;
using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Profile
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProdductRequest,Product>().ReverseMap();
            CreateMap<UpdateProdductRequest, Product>().ReverseMap();

            //Query
            CreateMap<GetProductDetailsVm, Product>().ReverseMap();
            CreateMap<GetProductListVm, Product>().ReverseMap();

        }
    }
}
