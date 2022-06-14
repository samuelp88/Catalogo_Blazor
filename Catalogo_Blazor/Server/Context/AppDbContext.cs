using Catalogo_Blazor.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Blazor.Server.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
