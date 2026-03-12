
using Demo.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Identity.Context
{
    public class DemoIdentityContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
    
        public DemoIdentityContext(DbContextOptions<DemoIdentityContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");

            builder.Entity<ApplicationUser>(b =>
            {
                b.HasMany(a => a.UserRoles)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey(a => a.UserId)
                .IsRequired();
            });
            builder.Entity<ApplicationRole>(b =>
            {
                b.HasMany(a => a.UserRoles)
                .WithOne(a => a.ApplicationRole)
                .HasForeignKey(a => a.RoleId)
                .IsRequired();
            });
            builder.ApplyConfigurationsFromAssembly(typeof(DemoIdentityContext).Assembly);
        }
    }
}
