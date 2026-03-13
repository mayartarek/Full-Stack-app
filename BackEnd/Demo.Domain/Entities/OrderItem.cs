using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class OrderItem:BaseEntity
    {

        public decimal Price { get; set; }
        //for all datat related to product save as json
        public decimal DiscontPercentag { get; set; }
        public Guid ProductId { get; set; }
        public string ProductDetailsJson { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

    }
}
