using Microsoft.EntityFrameworkCore;
using TrackingBle.src._3FloorplanDevice.Models.Domain;

namespace TrackingBle.src._3FloorplanDevice.Data
{
    public class FloorplanDeviceDbContext : DbContext
    {
        public FloorplanDeviceDbContext(DbContextOptions<FloorplanDeviceDbContext> options) : base(options) { }

        public DbSet<FloorplanDevice> FloorplanDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FloorplanDevice>(entity =>
            {
                entity.ToTable("floorplan_device");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Type).HasConversion(v => v.ToString().ToLower(), v => (DeviceType)Enum.Parse(typeof(DeviceType), v, true));
                entity.Property(e => e.FloorplanId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.AccessCctvId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ReaderId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.AccessControlId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.FloorplanMaskedAreaId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(255).IsRequired();
                entity.Property(e => e.UpdatedBy).HasMaxLength(255).IsRequired();
                entity.Property(e => e.DeviceStatus).HasConversion(v => v.ToString().ToLower(), v => (DeviceStatus)Enum.Parse(typeof(DeviceStatus), v, true));
                entity.Property(e => e.Status).IsRequired();
                entity.HasOne().WithMany().HasForeignKey(e => e.FloorplanId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne().WithMany().HasForeignKey(e => e.AccessCctvId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne().WithMany().HasForeignKey(e => e.ReaderId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne().WithMany().HasForeignKey(e => e.AccessControlId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne().WithMany().HasForeignKey(e => e.FloorplanMaskedAreaId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne().WithMany().HasForeignKey(e => e.ApplicationId).OnDelete(DeleteBehavior.NoAction);
                entity.HasIndex(e => e.Generate).IsUnique();
                entity.HasQueryFilter(e => e.Status != 0);
            });
        }
    }
}