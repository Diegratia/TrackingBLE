using Microsoft.EntityFrameworkCore;
using TrackingBle.src._8MstBleReader.Models.Domain;

namespace TrackingBle.src._8MstBleReader.Data
{
    public class MstBleReaderDbContext : DbContext
    {
        public MstBleReaderDbContext(DbContextOptions<MstBleReaderDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstBleReader> MstBleReaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstBleReader>(entity =>
            {
                entity.ToTable("mst_ble_reader");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate)
                      .ValueGeneratedOnAdd() // Auto-increment
                      .IsRequired();
                  //     .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 

                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .IsRequired();

                entity.Property(e => e.BrandId)
                      .HasColumnName("BrandId")
                      .IsRequired();

                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Mac)
                      .HasColumnName("Mac")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Ip)
                      .HasColumnName("Ip")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.LocationX)
                      .HasColumnName("LocationX")
                      .HasColumnType("decimal(18,6)")
                      .IsRequired();

                entity.Property(e => e.LocationY)
                      .HasColumnName("LocationY")
                      .HasColumnType("decimal(18,6)")
                      .IsRequired();

                entity.Property(e => e.LocationPxX)
                      .HasColumnName("LocationPxX")
                      .IsRequired();

                entity.Property(e => e.LocationPxY)
                      .HasColumnName("LocationPxY")
                      .IsRequired();

                entity.Property(e => e.EngineReaderId)
                      .HasColumnName("EngineReaderId")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.CreatedBy)
                      .HasColumnName("CreatedBy")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("CreatedAt")
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.UpdatedBy)
                      .HasColumnName("UpdatedBy")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.UpdatedAt)
                      .HasColumnName("UpdatedAt")
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnName("Status")
                      .IsRequired();
            });
        }
    }
}