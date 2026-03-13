using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Order.GetOrdersList
{
    public class GetOrdersListQuery:IRequest<List<GetOrdersListvm>>
    {
    }
}
