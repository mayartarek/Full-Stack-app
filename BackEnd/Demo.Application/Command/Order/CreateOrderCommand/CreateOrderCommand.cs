using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command.Order.CreateOrderCommand
{
    public class CreateOrderCommand:IRequest<bool>      

    {
        public List<CreateOrderitemVm> OrderItem { get; set; }
        public CreateUserDetailsVm CreateUserDetails { get; set; }
    }
}
