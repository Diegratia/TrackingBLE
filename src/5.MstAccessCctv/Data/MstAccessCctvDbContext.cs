using Microsoft.EntityFrameworkCore;
using TrackingBle.src._5MstAccessCctv.Models.Domain;

namespace TrackingBle.src._5MstAccessCctv.Data
{
    public class MstAccessCctvDbContext : DbContext
    {
        public MstAccessCctvDbContext(DbContextOptions<MstAccessCctvDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstAccessCctv> MstAccessCctvs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstAccessCctv>(entity =>
            {
                entity.Property(e => e.Generate)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Rtsp)
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

                entity.Property(e => e.IntegrationId)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired();

                entity.ToTable("mst_access_cctv");
                entity.HasKey(e => e.Id);
            });
        }
    }
}