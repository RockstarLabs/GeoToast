using GeoToast.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoToast.Data
{
    public class GeoToastDbContext : DbContext
    {
        public DbSet<Notification> Notifications { get; set; }
        
        public DbSet<Property> Properties { get; set; }

        public GeoToastDbContext(DbContextOptions<GeoToastDbContext> options)
             : base(options)
         {
         }
 
         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<Property>()
                .HasIndex(p => p.UserId);
         }
    }
}