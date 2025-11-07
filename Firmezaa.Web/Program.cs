using Firmezaa.Web.Data;
using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.Repositories.Interfaces;
using Firmezaa.Web.Repositories.Implementations;
using Firmezaa.Web.Services.Interfaces;
using Firmezaa.Web.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtener cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar DbContext con Npgsql (PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar Identity (con roles)
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 6;
        // Puedes personalizar más políticas aquí
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Registrar Repositorios y Servicios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Registrar MVC/Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Aplicar migraciones al iniciar y crear roles/usuario admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Aplicar migraciones
        var db = services.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        
        // Crear roles
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = new[] { "Administrador", "Cliente" };
        foreach (var role in roles)
        {
            if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            }
        }

        // Crear usuario administrador por defecto
        var userManager = services.GetRequiredService<UserManager<User>>();
        string adminEmail = "admin@firmeza.com";
        string adminPassword = "Admin123!";

        var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
        if (adminUser == null)
        {
            var user = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Administrador",
                EmailConfirmed = true
            };

            var result = userManager.CreateAsync(user, adminPassword).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Administrador").GetAwaiter().GetResult();
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error aplicando migraciones o creando roles al inicio");
        throw;
    }
}

// Pipeline estándar
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
