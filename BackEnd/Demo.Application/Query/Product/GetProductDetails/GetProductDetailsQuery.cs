using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Product.GetProductDetails
{
    public class GetProductDetailsQuery:IRequest<GetProductDetailsVm>
    {
        public Guid Id { get; set; }    
    }
}
