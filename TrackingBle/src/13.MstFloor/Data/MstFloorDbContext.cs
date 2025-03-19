using Microsoft.EntityFrameworkCore;
using TrackingBle.src._13MstFloor.Models.Domain;

namespace TrackingBle.src._13MstFloor.Data
{
    public class MstFloorDbContext : DbContext
    {
        public MstFloorDbContext(DbContextOptions<MstFloorDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstFloor> MstFloors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstFloor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.BuildingId).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FloorImage).HasMaxLength(255);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(50);
                entity.Property(e => e.UpdatedBy).HasMaxLength(50);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
                entity.HasQueryFilter(e => e.Status != 0);
            });
        }
    }
}