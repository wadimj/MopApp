using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.User;

namespace WebApplication1.Models
{
    public class MopContext : DbContext
    {
        public MopContext(DbContextOptions<MopContext> options) :base(options)
        {
        }
        
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ChartData> ChartDatas { get; set; }
        
        public DbSet<User.User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Temperature>().ToTable("Temperature");
            modelBuilder.Entity<Device>().ToTable("Devices");
            
            modelBuilder.Entity<UserRole>()
                .HasKey(t => new { t.UserId, t.RoleId });
        }
    }
}