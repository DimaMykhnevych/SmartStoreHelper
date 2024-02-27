using Microsoft.EntityFrameworkCore;
using SmartHelper.Models;

namespace SmartHelper.Context
{
    public class SmartHelperDbContext : DbContext
    {
        public SmartHelperDbContext(DbContextOptions<SmartHelperDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
