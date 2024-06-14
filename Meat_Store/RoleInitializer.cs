using Meat_Store.Models;
using Microsoft.AspNetCore.Identity;

namespace Meat_Store
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string name = "Illia";
            string surname = "Stetsenko";
            string phone_number = "099-999-9999";
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if(await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if(await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User 
                { 
                    Name = name,
                    Surname = surname,
                    PhoneNumber = phone_number,
                    Email = adminEmail, 
                    UserName = adminEmail 
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
