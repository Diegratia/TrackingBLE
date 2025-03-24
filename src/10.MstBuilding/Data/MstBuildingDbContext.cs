using Microsoft.EntityFrameworkCore;
using TrackingBle.src._10MstBuilding.Models.Domain;

namespace TrackingBle.src._10MstBuilding.Data
{
    public class MstBuildingDbContext : DbContext
    {
        public MstBuildingDbContext(DbContextOptions<MstBuildingDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstBuilding> MstBuildings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstBuilding>(entity =>
            {
                entity.ToTable("mst_building");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate)
                      .HasColumnName("_generate")
                      .ValueGeneratedOnAdd()
                      .IsRequired();
                    //   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore)

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .IsRequired();

                entity.Property(e => e.Name)
                      .HasColumnName("name")
                      .IsRequired();

                entity.Property(e => e.Image)
                      .HasColumnName("image")
                      .IsRequired();

                entity.Property(e => e.ApplicationId)
                      .HasColumnName("application_id")
                      .IsRequired();

                entity.Property(e => e.CreatedBy)
                      .HasColumnName("created_by")
                      .HasMaxLength(255)
                      .HasDefaultValue("system");

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("created_at")
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.UpdatedBy)
                      .HasColumnName("updated_by")
                      .HasMaxLength(255)
                      .HasDefaultValue("system");

                entity.Property(e => e.UpdatedAt)
                      .HasColumnName("updated_at")
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnName("status")
                      .IsRequired();
            });
        }
    }
}