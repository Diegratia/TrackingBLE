using Microsoft.EntityFrameworkCore;
using TrackingBle.src._15MstIntegration.Models.Domain;

namespace TrackingBle.src._15MstIntegration.Data
{
    public class MstIntegrationDbContext : DbContext
    {
        public MstIntegrationDbContext(DbContextOptions<MstIntegrationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstIntegration> MstIntegrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstIntegration>(entity =>
            {
                entity.Property(e => e.Generate)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.BrandId)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.IntegrationType)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(), // Enum ke string (lowercase)
                        v => (IntegrationType)Enum.Parse(typeof(IntegrationType), v, true) // String ke enum (ignore case)
                    );

                entity.Property(e => e.ApiTypeAuth)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(), // Enum ke string (lowercase)
                        v => (ApiTypeAuth)Enum.Parse(typeof(ApiTypeAuth), v, true) // String ke enum (ignore case)
                    );

                entity.Property(e => e.ApiUrl)
                    .IsRequired();

                entity.Property(e => e.ApiAuthUsername)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.ApiAuthPasswd)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.ApiKeyField)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.ApiKeyValue)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(36)
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
                    .IsRequired();

                entity.ToTable("mst_integration");
                entity.HasKey(e => e.Id);
            });
        }
    }
}