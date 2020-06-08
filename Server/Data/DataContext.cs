using Microsoft.EntityFrameworkCore;
using Server.Data.Models;

namespace Server.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
        }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

    }
}
