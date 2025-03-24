using Microsoft.EntityFrameworkCore;
using TrackingBle.src._6MstAccessControl.Models.Domain;

namespace TrackingBle.src._6MstAccessControl.Data
{
    public class MstAccessControlDbContext : DbContext
    {
        public MstAccessControlDbContext(DbContextOptions<MstAccessControlDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstAccessControl> MstAccessControls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstAccessControl>(entity =>
            {
                entity.Property(e => e.Generate)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.ControllerBrandId)
                    .HasMaxLength(36)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .IsRequired();

                entity.Property(e => e.Channel)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.DoorId)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Raw)
                    .IsRequired();

                entity.Property(e => e.IntegrationId)
                    .HasMaxLength(36)
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

                entity.ToTable("mst_access_control");
                entity.HasKey(e => e.Id);
            });
        }
    }
}