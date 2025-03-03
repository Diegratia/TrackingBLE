using System;
using Microsoft.EntityFrameworkCore;
using TrackingBle.Models.Domain;

namespace TrackingBle.Data
{
    public class TrackingBleDbContext : DbContext
    {
        public TrackingBleDbContext(DbContextOptions<TrackingBleDbContext> dbContextOptions) 
            : base(dbContextOptions) { }

        public DbSet<MstAccessCctv> MstAccessCctvs { get; set; }
        public DbSet<MstApplication> MstApplications { get; set; }
        public DbSet<MstIntegration> MstIntegrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure MstApplication
            modelBuilder.Entity<MstApplication>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsRequired();
            });

            // Configure MstIntegration and its relationship with MstApplication
            modelBuilder.Entity<MstIntegration>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsRequired();
                
                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(32) // Matches MstApplication.Id
                    .IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany(a => a.Integrations)
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Configure MstAccessCctv and its relationships
            modelBuilder.Entity<MstAccessCctv>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsRequired();
                
                entity.Property(e => e.IntegrationId)
                    .HasMaxLength(32) // Matches MstIntegration.Id
                    .IsRequired();
                
                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(32) // Matches MstApplication.Id
                    .IsRequired();

                entity.HasOne(m => m.Integration) // Singular, not Integrations
                    .WithMany()
                    .HasForeignKey(m => m.IntegrationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}