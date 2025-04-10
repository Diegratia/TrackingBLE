using Microsoft.EntityFrameworkCore;
using TrackingBle.src._1Auth.Models.Domain;

namespace TrackingBle.src._1Auth.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Generate).HasColumnName("_generate").ValueGeneratedOnAdd();
                entity.Property(e => e.Id).HasColumnName("id").HasMaxLength(255);
                entity.Property(e => e.Username).HasColumnName("username").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).HasColumnName("password").IsRequired().HasMaxLength(255);
                entity.Property(e => e.IsCreatedPassword).HasColumnName("is_created_password").IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
                entity.Property(e => e.IsEmailConfirmation).HasColumnName("is_email_confirmation").IsRequired();
                entity.Property(e => e.EmailConfirmationCode).HasColumnName("email_confirmation_code").IsRequired().HasMaxLength(255);
                entity.Property(e => e.EmailConfirmationExpiredAt).HasColumnName("email_confirmation_expired_at").IsRequired();
                entity.Property(e => e.EmailConfirmationAt).HasColumnName("email_confirmation_at").IsRequired();
                entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at").IsRequired();
                entity.Property(e => e.StatusActive).HasColumnName("status_active").IsRequired().HasConversion<int>();
                entity.Property(e => e.GroupId).HasColumnName("group_id").IsRequired();
                entity.HasOne(e => e.Group).WithMany(g => g.Users).HasForeignKey(e => e.GroupId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.ToTable("user_group");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.LevelPriority).HasColumnName("level_priority").IsRequired().HasConversion<int>();
                entity.Property(e => e.ApplicationId).HasColumnName("application_id").IsRequired();
                entity.Property(e => e.CreatedBy).HasColumnName("created_by").IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by").IsRequired().HasMaxLength(255);
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
                entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue(1);
                entity.HasOne().WithMany().HasForeignKey(e => e.ApplicationId).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}