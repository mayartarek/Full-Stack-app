using Demo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Constract.Interface
{
    public interface IUnitOfWork
    {
        IRepositoryPattern<Order> Orders { get; }
        IRepositoryPattern<Product> Products { get; }
        IRepositoryPattern<OrderItem> OrderItems { get; }
        Task<int> SaveChangesAsync();
        Task<int> AddOrder(Order order);
        Task<bool> CheckAvailabilty(Guid productId,int count);
        Task ReduceAvailabilty(Guid productId, int count);
    }
}
