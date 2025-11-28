using Microsoft.AspNetCore.Identity;

namespace Firmezaa.Api.Data.Seeders;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // Crear roles si no existen
        string[] roles = { "Admin", "Client" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Crear usuario administrador si no existe
        string adminEmail = "admin@firmeza.com";
        string adminPassword = "Admin123$";

        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}