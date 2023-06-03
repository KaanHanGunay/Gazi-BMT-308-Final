using System;
using Gazi_BMT_308_Final.Models;
using Microsoft.AspNetCore.Identity;

namespace Gazi_BMT_308_Final
{
    public static class UserAndRoleDataInitializer
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                User user = new User();
                user.UserName = "admin";
                user.Email = "admin@domain.com";

                IdentityResult result = userManager.CreateAsync(user, "Admin123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        private static async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("User"))
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "User";
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }
        }
    }

}

