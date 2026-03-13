using Demo.Application.Constract.Interface;
using Demo.Domain.Entities;
using Demo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Demo.Presintance.Repoitory
{
    public class UnitOfWork : IUnitOfWork
    {

        public IRepositoryPattern<Order> Orders { get; }
        public IRepositoryPattern<OrderItem> OrderItems { get; }
        private readonly DemoDbContext _context;
        public IRepositoryPattern<Product> Products { get; }

        public UnitOfWork(DemoDbContext context)
        {
            _context = context;
            Orders = new Repository<Order>(_context);
            OrderItems = new Repository<OrderItem>(_context);
            Products= new Repository<Product>(_context);    
        }
        Task<int> IUnitOfWork.AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IUnitOfWork.CheckAvailabilty(Guid productId, int count)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
                return false;
            return product.Stock >= count;
        }
        async Task IUnitOfWork.ReduceAvailabilty(Guid productId, int count)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
         
             product.Stock  =product.Stock-1;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
