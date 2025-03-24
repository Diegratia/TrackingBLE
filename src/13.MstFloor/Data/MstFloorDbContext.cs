using Microsoft.EntityFrameworkCore;
using TrackingBle.src._13MstFloor.Models.Domain;

namespace TrackingBle.src._13MstFloor.Data
{
    public class MstFloorDbContext : DbContext
    {
        public MstFloorDbContext(DbContextOptions<MstFloorDbContext> options) : base(options) { }

        public DbSet<MstFloor> MstFloors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstFloor>(entity =>
            {
                entity.ToTable("mst_floor"); // Nama tabel huruf kecil dengan underscore
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate)
                      .ValueGeneratedOnAdd()
                      .IsRequired();

                // entity.Property(e => e.Id)
                //       .HasDefaultValueSql("NEWID()")
                //       .IsRequired();

                entity.Property(e => e.BuildingId)
                      .IsRequired();

                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.FloorImage)
                      .HasMaxLength(255);

                entity.Property(e => e.PixelX)
                      .IsRequired();

                entity.Property(e => e.PixelY)
                      .IsRequired();

                entity.Property(e => e.FloorX)
                      .IsRequired();

                entity.Property(e => e.FloorY)
                      .IsRequired();

                entity.Property(e => e.MeterPerPx)
                      .IsRequired();

                entity.Property(e => e.EngineFloorId)
                      .IsRequired();

                entity.Property(e => e.CreatedBy)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .IsRequired();

                entity.Property(e => e.UpdatedBy)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.UpdatedAt)
                      .IsRequired();

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasDefaultValue(1);

                entity.HasQueryFilter(e => e.Status != 0); // Soft delete filter
            });
        }
    }
}