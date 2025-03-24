using Microsoft.EntityFrameworkCore;
using TrackingBle.src._11MstDepartment.Models.Domain;

namespace TrackingBle.src._11MstDepartment.Data
{
    public class MstDepartmentDbContext : DbContext
    {
        public MstDepartmentDbContext(DbContextOptions<MstDepartmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstDepartment> MstDepartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstDepartment>(entity =>
            {
                entity.ToTable("mst_department");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate)
                      .ValueGeneratedOnAdd()
                      .IsRequired();
                    //   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

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

                entity.Property(e => e.DepartmentHost)
                      .HasColumnName("DepartmentHost")
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