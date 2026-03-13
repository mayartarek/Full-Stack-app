using Demo.Application.Constract.Interface;
using Demo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query.Order.GetOrdersList
{
    public class GetOrdersListQueryHander : IRequestHandler<GetOrdersListQuery, List<GetOrdersListvm>>
    {
        private readonly IRepositoryPattern<Domain.Entities.Order> repository;
        private readonly ILoggedInerface loggedInerface;

        public GetOrdersListQueryHander(IRepositoryPattern<Demo.Domain.Entities.Order> repository,ILoggedInerface loggedInerface)
        {
            this.repository = repository;
            this.loggedInerface = loggedInerface;
        }
        async Task<List<GetOrdersListvm>> IRequestHandler<GetOrdersListQuery, List<GetOrdersListvm>>.Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var product = await this.repository.GetAllAsync();
                product=product.Where(a=>a.CreatedBy==this.loggedInerface.UserId).ToList();
            var result = new List<GetOrdersListvm>();   
            foreach (var item in product)
            {
                GetOrdersListvm getOrdersListvm = new GetOrdersListvm();
                getOrdersListvm.Id = item.Id;
                getOrdersListvm.Email = item.Email;
                getOrdersListvm.PhoneNumber = item.PhoneNumber;
                getOrdersListvm.ShippingAddress = item.ShippingAddress;
                getOrdersListvm.TotalPrice = item.TotalPrice;
                result.Add(getOrdersListvm);    
            }
            return result;
        }
    }
}
