using Microsoft.EntityFrameworkCore;
using TrackingBle.src._1Auth.Models.Domain;

namespace TrackingBle.src._1Auth.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Generate).ValueGeneratedOnAdd();
                entity.Property(e => e.StatusActive)
                   .HasColumnType("int");
                entity.HasOne(e => e.Group)
                      .WithMany(g => g.Users)
                      .HasForeignKey(e => e.GroupId);
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.ToTable("user_group");
                entity.HasKey(e => e.Id);
               entity.Property(e => e.LevelPriority) // Simpan sebagai int
                .HasColumnType("int");
            });
        }
    }
}