using Microsoft.EntityFrameworkCore;
using TrackingBle.src._19Visitor.Models.Domain;

namespace TrackingBle.src._19Visitor.Data
{
    public class VisitorDbContext : DbContext
    {
        public VisitorDbContext(DbContextOptions<VisitorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visitor> Visitors { get; set; }
        // public DbSet<MstApplication> MstApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.ToTable("visitor");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Generate).ValueGeneratedOnAdd().IsRequired();
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.PersonId).HasColumnName("PersonId").HasMaxLength(255).IsRequired();
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
                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationId").IsRequired();
                entity.Property(e => e.RegisteredDate).HasColumnName("RegisteredDate").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.VisitorArrival).HasColumnName("VisitorArrival").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.VisitorEnd).HasColumnName("VisitorEnd").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.PortalKey).HasColumnName("PortalKey").IsRequired();
                entity.Property(e => e.TimestampPreRegistration).HasColumnName("TimestampPreRegistration").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.TimestampCheckedIn).HasColumnName("TimestampCheckedIn").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.TimestampCheckedOut).HasColumnName("TimestampCheckedOut").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.TimestampDeny).HasColumnName("TimestampDeny").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.TimestampBlocked).HasColumnName("TimestampBlocked").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.TimestampUnblocked).HasColumnName("TimestampUnblocked").HasColumnType("datetime").IsRequired();
                entity.Property(e => e.CheckinBy).HasColumnName("CheckinBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.CheckoutBy).HasColumnName("CheckoutBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.DenyBy).HasColumnName("DenyBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.BlockBy).HasColumnName("BlockBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.UnblockBy).HasColumnName("UnblockBy").HasMaxLength(255).IsRequired();
                entity.Property(e => e.ReasonDeny).HasColumnName("ReasonDeny").HasMaxLength(255).IsRequired();
                entity.Property(e => e.ReasonBlock).HasColumnName("ReasonBlock").HasMaxLength(255).IsRequired();
                entity.Property(e => e.ReasonUnblock).HasColumnName("ReasonUnblock").HasMaxLength(255).IsRequired();
                  entity.Property(e => e.Status)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (VisitorStatus)Enum.Parse(typeof(VisitorStatus), v, true)
                    );
                entity.HasIndex(v => v.PersonId);
                entity.HasIndex(v => v.Email);
            });
        }
    }
}