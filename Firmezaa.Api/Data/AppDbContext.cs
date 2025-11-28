using Firmezaa.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Firmezaa.Api.Data;

public class AppDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleProduct> SaleProducts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}