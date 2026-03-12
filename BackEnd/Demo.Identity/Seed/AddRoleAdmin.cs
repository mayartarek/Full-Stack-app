using Demo.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Identity.Seed
{
    public class AddRoleAdmin
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roleToAdd = new ApplicationRole
            {
                Name = "Admin"
            };


            var role = await roleManager.FindByNameAsync(roleToAdd.Name);
            if (role == null)
            {
                await roleManager.CreateAsync(roleToAdd);
            }
        }
    }

    }

