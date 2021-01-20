using Microsoft.EntityFrameworkCore;

namespace webApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<City> Cities { get; set; }
    }
}