using Microsoft.EntityFrameworkCore;
using TrackingBle.src._7MstApplication.Models.Domain;

namespace TrackingBle.src._7MstApplication.Data
{
    public class MstApplicationDbContext : DbContext
    {
        public MstApplicationDbContext(DbContextOptions<MstApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstApplication> MstApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstApplication>(entity =>
            {
                entity.Property(e => e.Generate)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.ApplicationName)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.OrganizationType)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (OrganizationType)Enum.Parse(typeof(OrganizationType), v, true)
                    );

                entity.Property(e => e.OrganizationAddress)
                    .IsRequired();

                entity.Property(e => e.ApplicationType)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (ApplicationType)Enum.Parse(typeof(ApplicationType), v, true)
                    );

                entity.Property(e => e.ApplicationRegistered)
                    .IsRequired();

                entity.Property(e => e.ApplicationExpired)
                    .IsRequired();

                entity.Property(e => e.HostName)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.HostPhone)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.HostEmail)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.HostAddress)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.ApplicationCustomName)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.ApplicationCustomDomain)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.ApplicationCustomPort)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.LicenseCode)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.LicenseType)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (LicenseType)Enum.Parse(typeof(LicenseType), v, true)
                    );

                entity.Property(e => e.ApplicationStatus)
                    .IsRequired();

                entity.ToTable("mst_application");
                entity.HasKey(e => e.Id);
            });
        }
    }
}