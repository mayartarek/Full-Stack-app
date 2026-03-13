using Demo.Application.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Product.GetProductList
{
    public class GetProductListQuery:IRequest<PaginationList<GetProductListVm>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetProductListFilter Filter { get;set; } = new GetProductListFilter();

    }
}
