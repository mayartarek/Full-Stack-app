using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command.Product.UpdateProduct
{
    public class UpdateProdductRequest:IRequest<bool>
    {
        public Guid Id { get; set; }    
        public decimal Price { get; set; }
        public float DiscountPercentage { get; set; }
        public string Description { get; set; }
        public IFormFile? FormFile { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
