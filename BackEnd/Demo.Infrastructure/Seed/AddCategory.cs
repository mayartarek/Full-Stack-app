using Demo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Presintance.Seed
{
    public static class AddCategory
    {
        //can make crud opertions here for category  
        public static async Task AddCategorySeed(DemoDbContext demoDbContext)
        {
            var Categories = new List<Domain.Entities.Category>()
            {
                new Domain.Entities.Category()
                {
                    Id=Guid.NewGuid(),
                    Name="Category 1",
                },
                new Domain.Entities.Category()
                {
                    Id=Guid.NewGuid(),
                    Name="Category 2",
                },
                new Domain.Entities.Category()
                {
                    Id=Guid.NewGuid(),
                    Name="Category 3",
                }
            };
            foreach (var category in Categories) { 
            var item = await demoDbContext.Categories.Where(c => c.Name == category.Name).FirstOrDefaultAsync();
                if (item == null)
                {
                    await demoDbContext.Categories.AddAsync(category);
                    await demoDbContext.SaveChangesAsync();
                }
               
            }
        }
         
    } 
}
