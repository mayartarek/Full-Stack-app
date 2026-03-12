using Demo.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Identity.Seed
{
    public class AddFirstUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName ="Admin",
                LastName = "Seed Data",
                UserName = "Admin@gmail.com",
                Email = "Admin@gmail.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(applicationUser, "P@ssw0rd");
            }
            user = await userManager.FindByEmailAsync(applicationUser?.Email);
            if (!await userManager.IsInRoleAsync(user,"Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
