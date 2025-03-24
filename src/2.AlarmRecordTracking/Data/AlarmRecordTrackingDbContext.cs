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

                entity.Property(e => e.Generate).ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.VisitorId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ReaderId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.FloorplanMaskedAreaId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.Alarm).IsRequired().HasConversion(v => v.ToString().ToLower(), v => (AlarmRecordStatus)Enum.Parse(typeof(AlarmRecordStatus), v, true));
                entity.Property(e => e.Action).IsRequired().HasConversion(v => v.ToString().ToLower(), v => (ActionStatus)Enum.Parse(typeof(ActionStatus), v, true));
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.IdleTimestamp).IsRequired();
                entity.Property(e => e.DoneTimestamp).IsRequired();
                entity.Property(e => e.CancelTimestamp).IsRequired();
                entity.Property(e => e.WaitingTimestamp).IsRequired();
                entity.Property(e => e.InvestigatedTimestamp).IsRequired();
                entity.Property(e => e.InvestigatedDoneAt).IsRequired();
                entity.Property(e => e.IdleBy).HasMaxLength(255);
                entity.Property(e => e.DoneBy).HasMaxLength(255);
                entity.Property(e => e.CancelBy).HasMaxLength(255).IsRequired();
                entity.Property(e => e.WaitingBy).HasMaxLength(255).IsRequired();
                entity.Property(e => e.InvestigatedBy).HasMaxLength(255).IsRequired();
                entity.Property(e => e.InvestigatedResult).IsRequired();

                entity.HasIndex(e => e.Generate).IsUnique();
            });
        }
    }
}