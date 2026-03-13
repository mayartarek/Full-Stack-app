using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Product.GetProductList
{
    public class GetProductListVm
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public float DiscountPercentage { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
    }
}
