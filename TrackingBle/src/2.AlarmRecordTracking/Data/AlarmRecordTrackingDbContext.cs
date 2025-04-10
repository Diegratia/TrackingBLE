using Microsoft.EntityFrameworkCore;
using TrackingBle.src._2AlarmRecordTracking.Models.Domain;

namespace TrackingBle.src._2AlarmRecordTracking.Data
{
    public class AlarmRecordTrackingDbContext : DbContext
    {
        public AlarmRecordTrackingDbContext(DbContextOptions<AlarmRecordTrackingDbContext> options) : base(options) { }

        public DbSet<AlarmRecordTracking> AlarmRecordTrackings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlarmRecordTracking>(entity =>
            {
                entity.ToTable("alarm_record_tracking");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.VisitorId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ReaderId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.FloorplanMaskedAreaId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.Alarm).HasColumnName("alarm_record_status").HasColumnType("nvarchar(255)").IsRequired()
                    .HasConversion(v => v.ToString().ToLower(), v => (AlarmRecordStatus)Enum.Parse(typeof(AlarmRecordStatus), v, true));
                entity.Property(e => e.Action).HasColumnType("nvarchar(255)").IsRequired()
                    .HasConversion(v => v.ToString().ToLower(), v => (ActionStatus)Enum.Parse(typeof(ActionStatus), v, true));
                entity.HasOne().WithMany().HasForeignKey(e => e.VisitorId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne().WithMany().HasForeignKey(e => e.ReaderId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne().WithMany().HasForeignKey(e => e.FloorplanMaskedAreaId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne().WithMany().HasForeignKey(e => e.ApplicationId).OnDelete(DeleteBehavior.NoAction);
                entity.HasIndex(e => e.Generate).IsUnique().HasDatabaseName("alarm_record_tracking__generate_unique");
            });
        }
    }
}