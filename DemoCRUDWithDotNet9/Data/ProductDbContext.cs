using DemoCRUDWithDotNet9.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoCRUDWithDotNet9.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) :
        DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
    }
}
