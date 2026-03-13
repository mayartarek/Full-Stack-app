using Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Context
{
    public class DemoDbContext:DbContext
    {
     
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options) 
        {
            
        }
       public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<Domain.Entities.OrderItem> OrderItems { get; set; }
        public DbSet<Domain.Entities.Category> Categories { get; set; }
        public DbSet<Domain.Entities.Product> Products { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
