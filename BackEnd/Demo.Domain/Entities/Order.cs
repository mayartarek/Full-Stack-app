using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class Order:BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    }
}
