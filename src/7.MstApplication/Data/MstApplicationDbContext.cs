using Microsoft.EntityFrameworkCore;
using TrackingBle.src._7MstApplication.Models.Domain;

namespace TrackingBle.src._7MstApplication.Data
{
    public class MstApplicationDbContext : DbContext
    {
        public DbSet<MstApplication> MstApplications { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }

        public MstApplicationDbContext(DbContextOptions<MstApplicationDbContext> options)
            : base(options)
        {
        }

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
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasDefaultValue(OrganizationType.Single);

                entity.Property(e => e.OrganizationAddress)
                    .IsRequired();

                entity.Property(e => e.ApplicationType)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasDefaultValue(ApplicationType.Empty)
                    .HasConversion(
                        v => v == ApplicationType.Empty ? "" : v.ToString().ToLower(),
                        v => string.IsNullOrEmpty(v) ? ApplicationType.Empty : (ApplicationType)Enum.Parse(typeof(ApplicationType), v, true)
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
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (LicenseType)Enum.Parse(typeof(LicenseType), v, true)
                    );

                entity.Property(e => e.ApplicationStatus)
                    .IsRequired()
                    .HasDefaultValue(1);

                entity.ToTable("mst_application");
                entity.HasKey(e => e.Id);
            });

            // Konfigurasi UserGroup
            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.LevelPriority)
                    .HasColumnName("level_priority")
                    .HasColumnType("int");

                entity.Property(e => e.ApplicationId)
                    .HasColumnName("application_id")
                    .IsRequired();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValue(1);

                entity.HasOne<MstApplication>()
                    .WithMany()
                    .HasForeignKey(e => e.ApplicationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("user_group");
                entity.HasKey(e => e.Id);
            });

            // Konfigurasi User
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Generate)
                    .HasColumnName("_generate")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.IsCreatedPassword)
                    .HasColumnName("is_created_password")
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.IsEmailConfirmation)
                    .HasColumnName("is_email_confirmation")
                    .IsRequired();

                entity.Property(e => e.EmailConfirmationCode)
                    .HasColumnName("email_confirmation_code")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.EmailConfirmationExpiredAt)
                    .HasColumnName("email_confirmation_expired_at")
                    .IsRequired();

                entity.Property(e => e.EmailConfirmationAt)
                    .HasColumnName("email_confirmation_at")
                    .IsRequired();

                entity.Property(e => e.LastLoginAt)
                    .HasColumnName("last_login_at")
                    .IsRequired();

                   entity.Property(e => e.StatusActive)
                   .HasColumnName("status_active")
                   .HasColumnType("int");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .IsRequired();

                entity.HasOne(u => u.Group)
                    .WithMany(ug => ug.Users)
                    .HasForeignKey(u => u.GroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("user");
                entity.HasKey(e => e.Id);
            });
        }
    }
}