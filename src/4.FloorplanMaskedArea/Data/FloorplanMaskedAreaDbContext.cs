using Microsoft.EntityFrameworkCore;
using TrackingBle.src._4FloorplanMaskedArea.Models.Domain;

namespace TrackingBle.src._4FloorplanMaskedArea.Data
{
    public class FloorplanMaskedAreaDbContext : DbContext
    {
        public FloorplanMaskedAreaDbContext(DbContextOptions<FloorplanMaskedAreaDbContext> options) : base(options) { }

        public DbSet<FloorplanMaskedArea> FloorplanMaskedAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FloorplanMaskedArea>(entity =>
            {
                entity.ToTable("floorplan_masked_area");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Generate).ValueGeneratedOnAdd();
                entity.Property(e => e.FloorplanId).IsRequired();
                entity.Property(e => e.FloorId).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
                entity.Property(e => e.AreaShape).IsRequired();
                entity.Property(e => e.ColorArea).HasMaxLength(255).IsRequired();
                entity.Property(e => e.RestrictedStatus)
                      .HasColumnType("nvarchar(255)")
                      .IsRequired()
                      .HasConversion(
                          v => v == RestrictedStatus.NonRestrict ? "non-restrict" : v.ToString().ToLower(),
                          v => v == "non-restrict" ? RestrictedStatus.NonRestrict : (RestrictedStatus)Enum.Parse(typeof(RestrictedStatus), v, true)
                      );
                entity.Property(e => e.EngineAreaId).HasMaxLength(255).IsRequired();
                entity.Property(e => e.WideArea).IsRequired();
                entity.Property(e => e.PositionPxX).IsRequired();
                entity.Property(e => e.PositionPxY).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(255).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedBy).HasMaxLength(255).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasDefaultValue(1);
                entity.HasQueryFilter(e => e.Status != 0);
            });
        }
    }
}