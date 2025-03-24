using Microsoft.EntityFrameworkCore;
using TrackingBle.src._9MstBrand.Models.Domain;
using TrackingBle.src._9MstBrand.Data;

namespace TrackingBle.src._9MstBrand.Data
{
    public class MstBrandDbContext : DbContext
    {
        public MstBrandDbContext(DbContextOptions<MstBrandDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstBrand> MstBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstBrand>(entity =>
            {
                entity.ToTable("mst_brand");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Generate)
                      .ValueGeneratedOnAdd()
                      .IsRequired();
                entity.Property(e => e.Name)
                      .HasMaxLength(255)
                      .IsRequired();
                entity.Property(e => e.Tag)
                      .HasMaxLength(255)
                      .IsRequired();
                entity.Property(e => e.Status)
                      .IsRequired();

            //     entity.HasMany(e => e.Integrations)
            //           .WithOne()
            //           .HasForeignKey("BrandId")
            //           .OnDelete(DeleteBehavior.Cascade);
            //     entity.HasMany(e => e.BleReaders)
            //           .WithOne()
            //           .HasForeignKey("BrandId")
            //           .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}