using Microsoft.EntityFrameworkCore;
using TrackingBle.src._12MstDistrict.Models.Domain;

namespace TrackingBle.src._12MstDistrict.Data
{
    public class MstDistrictDbContext : DbContext
    {
        public MstDistrictDbContext(DbContextOptions<MstDistrictDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstDistrict> MstDistricts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstDistrict>(entity =>
            {
                entity.ToTable("mst_district");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate)
                      .ValueGeneratedOnAdd()
                      .IsRequired();
                  //     .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore)

                entity.Property(e => e.Id)
                      .HasColumnName("Id")
                      .IsRequired();

                entity.Property(e => e.Code)
                      .HasColumnName("Code")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Name)
                      .HasColumnName("Name")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.DistrictHost)
                      .HasColumnName("DistrictHost")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.ApplicationId)
                      .HasColumnName("ApplicationId")
                      .IsRequired();

                entity.Property(e => e.CreatedBy)
                      .HasColumnName("CreatedBy")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnName("CreatedAt")
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.UpdatedBy)
                      .HasColumnName("UpdatedBy")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.UpdatedAt)
                      .HasColumnName("UpdatedAt")
                      .HasColumnType("datetime")
                      .IsRequired();

                entity.Property(e => e.Status)
                      .HasColumnName("Status")
                      .IsRequired();
            });
        }
    }
}