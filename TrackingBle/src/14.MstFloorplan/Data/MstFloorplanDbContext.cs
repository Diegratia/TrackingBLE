using Microsoft.EntityFrameworkCore;
using TrackingBle.src._14MstFloorplan.Models.Domain;

namespace TrackingBle.src._14MstFloorplan.Data
{
    public class MstFloorplanDbContext : DbContext
    {
        public MstFloorplanDbContext(DbContextOptions<MstFloorplanDbContext> options) : base(options) { }

        public DbSet<MstFloorplan> MstFloorplans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstFloorplan>(entity =>
            {
                entity.ToTable("mst_floorplan");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Generate).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
                entity.Property(e => e.FloorId).IsRequired();
                entity.Property(e => e.ApplicationId).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(255).HasDefaultValue("");
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedBy).HasMaxLength(255).HasDefaultValue("");
                entity.Property(e => e.UpdatedAt).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasDefaultValue(1);
                entity.HasQueryFilter(e => e.Status != 0);
            });
        }
    }
}