using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class TemperatureContext : DbContext
    {
        public TemperatureContext(DbContextOptions<TemperatureContext> options) :base(options)
        {
        }
        
        public DbSet<Temperature> Temperatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Temperature>().ToTable("Temperature");
        }
    }
}