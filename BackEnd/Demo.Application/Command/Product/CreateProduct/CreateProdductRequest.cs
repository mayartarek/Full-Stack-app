using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command.Product.CreateProduct
{
    public class CreateProdductRequest:IRequest<bool>
    {
        public decimal Price { get; set; }
        public float DiscountPercentage { get; set; }
        public string Description { get; set; }
        public IFormFile FormFile { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
