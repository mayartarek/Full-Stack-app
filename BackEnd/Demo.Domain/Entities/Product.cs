using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class Product:BaseEntity
    {
        public decimal Price { get; set; }
        public float DiscountPercentage { get; set; }
        public string Description { get; set; } 
        public string Image { get; set; }   = string.Empty;
        public string Name { get; set; }    
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
