using Microsoft.EntityFrameworkCore;
using TrackingBle.Models.Domain;

namespace TrackingBle.Data
{
    public class TrackingBleDbContext : DbContext
    {
        public TrackingBleDbContext(DbContextOptions<TrackingBleDbContext> dbContextOptions) 
            : base(dbContextOptions) { }

        // DbSets for all models
        public DbSet<MstAccessCctv> MstAccessCctvs { get; set; }
        public DbSet<MstApplication> MstApplications { get; set; }
        public DbSet<MstIntegration> MstIntegrations { get; set; }
        public DbSet<MstAccessControl> MstAccessControls { get; set; }
        public DbSet<MstBrand> MstBrands { get; set; }
        public DbSet<MstOrganization> MstOrganizations { get; set; }
        public DbSet<MstDepartment> MstDepartments { get; set; }
        public DbSet<MstDistrict> MstDistricts { get; set; }
        public DbSet<MstMember> MstMembers { get; set; }
        public DbSet<MstFloor> MstFloors { get; set; }
        public DbSet<MstArea> MstAreas { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<VisitorBlacklistArea> VisitorBlacklistAreas { get; set; }
        public DbSet<MstBleReader> MstBleReaders { get; set; }
        public DbSet<TrackingTransaction> TrackingTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MstApplication
            modelBuilder.Entity<MstApplication>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(32).IsRequired();
            });

            // MstIntegration
            modelBuilder.Entity<MstIntegration>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(32).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(32).IsRequired();
                entity.Property(e => e.BrandId).HasMaxLength(255).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany(a => a.Integrations)
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // MstAccessCctv
            modelBuilder.Entity<MstAccessCctv>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(32).IsRequired();
                entity.Property(e => e.IntegrationId).HasMaxLength(32).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(32).IsRequired();

                entity.HasOne(m => m.Integration)
                    .WithMany()
                    .HasForeignKey(m => m.IntegrationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // MstAccessControl
            modelBuilder.Entity<MstAccessControl>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(32).IsRequired();
                entity.Property(e => e.IntegrationId).HasMaxLength(32).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Integration)
                    .WithMany()
                    .HasForeignKey(m => m.IntegrationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // MstBrand
            modelBuilder.Entity<MstBrand>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
            });

            // MstOrganization
            modelBuilder.Entity<MstOrganization>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(32).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // MstDepartment
            modelBuilder.Entity<MstDepartment>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(32).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // MstDistrict
            modelBuilder.Entity<MstDistrict>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(32).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // MstMember
            modelBuilder.Entity<MstMember>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(32).IsRequired();
                entity.Property(e => e.OrganizationId).HasMaxLength(255).IsRequired();
                entity.Property(e => e.DepartmentId).HasMaxLength(255).IsRequired();
                entity.Property(e => e.DistrictId).HasMaxLength(255).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Organization)
                    .WithMany()
                    .HasForeignKey(m => m.OrganizationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Department)
                    .WithMany()
                    .HasForeignKey(m => m.DepartmentId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.District)
                    .WithMany()
                    .HasForeignKey(m => m.DistrictId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(m => m.PersonId);
                entity.HasIndex(m => m.Email);
            });

            // MstFloor
            modelBuilder.Entity<MstFloor>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.BuildingId).HasMaxLength(255).IsRequired();
            });

            // MstArea
            modelBuilder.Entity<MstArea>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.FloorId).HasMaxLength(255).IsRequired();

                entity.HasOne(m => m.Floor)
                    .WithMany()
                    .HasForeignKey(m => m.FloorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Visitor
            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(32).IsRequired();

                entity.HasOne(v => v.Application)
                    .WithMany()
                    .HasForeignKey(v => v.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(v => v.PersonId);
                entity.HasIndex(v => v.Email);
            });

            // VisitorBlacklistArea
            modelBuilder.Entity<VisitorBlacklistArea>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.AreaId).HasMaxLength(255).IsRequired();
                entity.Property(e => e.VisitorId).HasMaxLength(255).IsRequired();

                entity.HasOne(v => v.Area)
                    .WithMany()
                    .HasForeignKey(v => v.AreaId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(v => v.Visitor)
                    .WithMany()
                    .HasForeignKey(v => v.VisitorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // MstBleReader
            modelBuilder.Entity<MstBleReader>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.BrandId).HasMaxLength(255).IsRequired();

                entity.HasOne(m => m.Brand)
                    .WithMany()
                    .HasForeignKey(m => m.BrandId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // TrackingTransaction
            modelBuilder.Entity<TrackingTransaction>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255).IsRequired();
                entity.Property(e => e.ReaderId).HasMaxLength(255).IsRequired();
                entity.Property(e => e.AreaId).HasMaxLength(255).IsRequired();

                entity.HasOne(t => t.Reader)
                    .WithMany()
                    .HasForeignKey(t => t.ReaderId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(t => t.Area)
                    .WithMany()
                    .HasForeignKey(t => t.AreaId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}