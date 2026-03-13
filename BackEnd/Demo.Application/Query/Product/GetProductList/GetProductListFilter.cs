using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Product.GetProductList
{
    public class GetProductListFilter
    {
        public string? Name { get; set; }
       
        public List<Guid>? CategoryIds { get; set; }
        
    }
}
