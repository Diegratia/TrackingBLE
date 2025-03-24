using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrackingBle.src._16MstMember.Models.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TrackingBle.src._16MstMember.Data
{
    public class MstMemberDbContext : DbContext
    {
        public MstMemberDbContext(DbContextOptions<MstMemberDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstMember> MstMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstMember>(entity =>
            {
                entity.ToTable("mst_member");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate).ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.PersonId).HasColumnName("PersonId").HasMaxLength(255).IsRequired();
                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationId").IsRequired();
                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentId").IsRequired();
                entity.Property(e => e.DistrictId).HasColumnName("DistrictId").IsRequired();
                entity.Property(e => e.IdentityId).HasColumnName("IdentityId").HasMaxLength(255).IsRequired();
                entity.Property(e => e.CardNumber).HasColumnName("CardNumber").HasMaxLength(255).IsRequired();
                entity.Property(e => e.BleCardNumber).HasColumnName("BleCardNumber").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Phone).HasColumnName("Phone").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Gender)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (Gender)Enum.Parse(typeof(Gender), v, true)
                    );
                entity.Property(e => e.Address).HasColumnName("Address").IsRequired();
                entity.Property(e => e.FaceImage).HasColumnName("FaceImage").IsRequired();
                entity.Property(e => e.UploadFr).HasColumnName("UploadFr").IsRequired();
                entity.Property(e => e.UploadFrError).HasColumnName("UploadFrError").IsRequired();
                entity.Property(e => e.BirthDate).HasColumnName("BirthDate").IsRequired();
                entity.Property(e => e.JoinDate).HasColumnName("JoinDate").IsRequired();
                entity.Property(e => e.ExitDate).HasColumnName("ExitDate").IsRequired();
                entity.Property(e => e.HeadMember1).HasColumnName("HeadMember1").HasMaxLength(255).IsRequired();
                entity.Property(e => e.HeadMember2).HasColumnName("HeadMember2").HasMaxLength(255).IsRequired();
                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationId").IsRequired();
                entity.Property(e => e.StatusEmployee)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v == StatusEmployee.NonActive ? "non-active" : v.ToString().ToLower(),
                        v => v == "non-active" ? StatusEmployee.NonActive : (StatusEmployee)Enum.Parse(typeof(StatusEmployee), v, true)
                    );
                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.UpdatedBy).HasColumnName("UpdatedBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.Status).HasColumnName("Status").IsRequired();
            });
        }
    }
}