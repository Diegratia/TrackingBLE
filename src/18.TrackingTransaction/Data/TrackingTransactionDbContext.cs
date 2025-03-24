using Microsoft.EntityFrameworkCore;
using TrackingBle.src._18TrackingTransaction.Models.Domain;

namespace TrackingBle.src._18TrackingTransaction.Data
{
    public class TrackingTransactionDbContext : DbContext
    {
        public TrackingTransactionDbContext(DbContextOptions<TrackingTransactionDbContext> options)
            : base(options)
        {
        }

        public DbSet<TrackingTransaction> TrackingTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackingTransaction>(entity =>
            {
                entity.ToTable("tracking_transaction");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.TransTime).HasColumnName("TransTime").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.ReaderId).HasColumnName("ReaderId").IsRequired();
                entity.Property(e => e.CardId).HasColumnName("CardId").IsRequired();
                entity.Property(e => e.FloorplanMaskedAreaId).HasColumnName("FloorplanMaskedAreaId").IsRequired();
                entity.Property(e => e.CoordinateX).HasColumnName("CoordinateX").IsRequired();
                entity.Property(e => e.CoordinateY).HasColumnName("CoordinateY").IsRequired();
                entity.Property(e => e.CoordinatePxX).HasColumnName("CoordinatePxX").IsRequired();
                entity.Property(e => e.CoordinatePxY).HasColumnName("CoordinatePxY").IsRequired();
                entity.Property(e => e.AlarmStatus)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v == AlarmStatus.NonActive ? "non-active" : v.ToString().ToLower(),
                        v => v == "non-active" ? AlarmStatus.NonActive : (AlarmStatus)Enum.Parse(typeof(AlarmStatus), v, true)
                    );
                entity.Property(e => e.Battery).HasColumnName("Battery").IsRequired();
            });
        }
    }
}