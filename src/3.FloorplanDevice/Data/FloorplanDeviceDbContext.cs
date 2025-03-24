using Microsoft.EntityFrameworkCore;
using TrackingBle.src._3FloorplanDevice.Models.Domain;

namespace TrackingBle.src._3FloorplanDevice.Data
{
    public class FloorplanDeviceDbContext : DbContext
    {
        public FloorplanDeviceDbContext(DbContextOptions<FloorplanDeviceDbContext> options) : base(options) { }

        // DbSet untuk tabel FloorplanDevice
        public DbSet<FloorplanDevice> FloorplanDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FloorplanDevice>(entity =>
            {
                // Nama tabel di database
                entity.ToTable("floorplan_device");

                // Primary key
                entity.HasKey(e => e.Id);

                // Kolom Generate (auto-increment)
                entity.Property(e => e.Generate)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                // Kolom Id (GUID)
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsRequired();

                // Kolom Name
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                // Kolom Type (enum)
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (DeviceType)Enum.Parse(typeof(DeviceType), v, true)
                    );

                // Kolom FloorplanId (foreign key)
                entity.Property(e => e.FloorplanId)
                    .HasMaxLength(36)
                    .IsRequired();

                // Kolom AccessCctvId (foreign key)
                entity.Property(e => e.AccessCctvId)
                    .HasMaxLength(36)
                    .IsRequired();

                // Kolom ReaderId (foreign key)
                entity.Property(e => e.ReaderId)
                    .HasMaxLength(36)
                    .IsRequired();

                // Kolom AccessControlId (foreign key)
                entity.Property(e => e.AccessControlId)
                    .HasMaxLength(36)
                    .IsRequired();

                // Kolom PosX
                entity.Property(e => e.PosX)
                    .HasColumnType("decimal(18,6)") // Presisi untuk koordinat
                    .IsRequired();

                // Kolom PosY
                entity.Property(e => e.PosY)
                    .HasColumnType("decimal(18,6)")
                    .IsRequired();

                // Kolom PosPxX
                entity.Property(e => e.PosPxX)
                    .IsRequired();

                // Kolom PosPxY
                entity.Property(e => e.PosPxY)
                    .IsRequired();

                // Kolom FloorplanMaskedAreaId (foreign key)
                entity.Property(e => e.FloorplanMaskedAreaId)
                    .HasMaxLength(36)
                    .IsRequired();

                // Kolom ApplicationId (foreign key)
                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(36)
                    .IsRequired();

                // Kolom CreatedBy
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .IsRequired();

                // Kolom CreatedAt
                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                // Kolom UpdatedBy
                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(255)
                    .IsRequired();

                // Kolom UpdatedAt
                entity.Property(e => e.UpdatedAt)
                    .IsRequired();

                // Kolom DeviceStatus (enum)
                entity.Property(e => e.DeviceStatus)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (DeviceStatus)Enum.Parse(typeof(DeviceStatus), v, true)
                    );

                // Kolom Status
                entity.Property(e => e.Status)
                    .IsRequired();

                // Indeks unik untuk Generate
                entity.HasIndex(e => e.Generate)
                    .IsUnique();

                // Filter soft delete (hanya ambil data dengan Status != 0)
                entity.HasQueryFilter(e => e.Status != 0);

                // Relasi foreign key (tanpa navigasi, karena data diambil via API)
                // Jika ingin relasi navigasi, uncomment dan sesuaikan dengan domain terkait
                // entity.HasOne<MstFloorplan>().WithMany().HasForeignKey(e => e.FloorplanId).OnDelete(DeleteBehavior.NoAction);
                // entity.HasOne<MstAccessCctv>().WithMany().HasForeignKey(e => e.AccessCctvId).OnDelete(DeleteBehavior.NoAction);
                // entity.HasOne<MstBleReader>().WithMany().HasForeignKey(e => e.ReaderId).OnDelete(DeleteBehavior.NoAction);
                // entity.HasOne<MstAccessControl>().WithMany().HasForeignKey(e => e.AccessControlId).OnDelete(DeleteBehavior.NoAction);
                // entity.HasOne<FloorplanMaskedArea>().WithMany().HasForeignKey(e => e.FloorplanMaskedAreaId).OnDelete(DeleteBehavior.NoAction);
                // entity.HasOne<MstApplication>().WithMany().HasForeignKey(e => e.ApplicationId).OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}