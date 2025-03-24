using Microsoft.EntityFrameworkCore;
using TrackingBle.src._20VisitorBlacklistArea.Models.Domain;

namespace TrackingBle.src._20VisitorBlacklistArea.Data
{
    public class VisitorBlacklistAreaDbContext : DbContext
    {
        public VisitorBlacklistAreaDbContext(DbContextOptions<VisitorBlacklistAreaDbContext> options)
            : base(options)
        {
        }

        public DbSet<VisitorBlacklistArea> VisitorBlacklistAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitorBlacklistArea>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.FloorplanMaskedAreaId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.VisitorId).HasMaxLength(36).IsRequired();

                entity.ToTable("visitor_blacklist_area");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate)
                    .ValueGeneratedOnAdd()
                    .IsRequired();
            });
        }
    }
}