using Microsoft.EntityFrameworkCore;
using TrackingBle.src._17MstOrganization.Models.Domain;

namespace TrackingBle.src._17MstOrganization.Data
{
    public class MstOrganizationDbContext : DbContext
    {
        public MstOrganizationDbContext(DbContextOptions<MstOrganizationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstOrganization> MstOrganizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstOrganization>(entity =>
            {
                entity.ToTable("mst_organization");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate).ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.Code).HasColumnName("Code").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
                entity.Property(e => e.OrganizationHost).HasColumnName("OrganizationHost").HasMaxLength(255).IsRequired();
                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationId").IsRequired();
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.Status).HasColumnName("Status").IsRequired();
            });
        }
    }
}