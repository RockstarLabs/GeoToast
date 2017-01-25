using GeoToast.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoToast.Data
{
    public class GeoToastDbContext : DbContext
    {
        public DbSet<Website> Websites { get; set; }

        public GeoToastDbContext(DbContextOptions<GeoToastDbContext> options)
             : base(options)
         {
         }
 
         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<Website>()
                .HasIndex(w => w.UserId);
         }
    }
}