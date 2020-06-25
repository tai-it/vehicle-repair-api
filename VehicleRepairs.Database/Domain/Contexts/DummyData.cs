namespace VehicleRepairs.Database.Domain.Contexts
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context,
                              UserManager<User> userManager,
                              RoleManager<Role> roleManager)
        {
            context.Database.EnsureCreated();

            string password = "P@$$w0rd";

            if (await roleManager.FindByNameAsync(CommonConstants.Roles.SUPER_ADMIN) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = CommonConstants.Roles.SUPER_ADMIN });
            }
            if (await roleManager.FindByNameAsync(CommonConstants.Roles.ADMIN) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = CommonConstants.Roles.ADMIN });
            }
            if (await roleManager.FindByNameAsync(CommonConstants.Roles.STATION) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = CommonConstants.Roles.STATION });
            }
            if (await roleManager.FindByNameAsync(CommonConstants.Roles.USER) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = CommonConstants.Roles.USER });
            }

            if (await userManager.FindByNameAsync("0915981110") == null)
            {
                var user = new User
                {
                    UserName = "0915981110",
                    Email = "sa@suaxe.com",
                    PhoneNumber = "0915981110",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.SUPER_ADMIN);
                }
            }

            if (await userManager.FindByNameAsync("0123456789") == null)
            {
                var user = new User
                {
                    UserName = "0123456789",
                    Email = "admin@suaxe.com",
                    PhoneNumber = "0123456789",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.ADMIN);
                }
            }
        }
    }
}
