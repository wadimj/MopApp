using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class MopContext : DbContext
    {
        public MopContext(DbContextOptions<MopContext> options) :base(options)
        {
        }
        
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Temperature>().ToTable("Temperature");
            modelBuilder.Entity<Device>().ToTable("Devices");
        }
    }
}