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

            // Nama tabel sesuai SQL
            modelBuilder.Entity<MstApplication>().ToTable("mst_application");
            modelBuilder.Entity<MstIntegration>().ToTable("mst_integration");
            modelBuilder.Entity<MstAccessCctv>().ToTable("mst_access_cctv");
            modelBuilder.Entity<MstAccessControl>().ToTable("mst_access_control");
            modelBuilder.Entity<MstBrand>().ToTable("mst_brand");
            modelBuilder.Entity<MstOrganization>().ToTable("mst_organization");
            modelBuilder.Entity<MstDepartment>().ToTable("mst_department");
            modelBuilder.Entity<MstDistrict>().ToTable("mst_district");
            modelBuilder.Entity<MstMember>().ToTable("mst_member");
            modelBuilder.Entity<MstFloor>().ToTable("mst_floor");
            modelBuilder.Entity<MstArea>().ToTable("mst_area");
            modelBuilder.Entity<Visitor>().ToTable("visitor");
            modelBuilder.Entity<VisitorBlacklistArea>().ToTable("visitor_blacklist_area");
            modelBuilder.Entity<MstBleReader>().ToTable("mst_ble_reader");
            modelBuilder.Entity<TrackingTransaction>().ToTable("tracking_transaction");

            // MstApplication
        modelBuilder.Entity<MstApplication>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.OrganizationType)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasDefaultValue(OrganizationType.Single); // Gunakan enum langsung
                    // .HasConversion(
                    //     v => v.ToString().ToLower(), // Simpan ke DB sebagai "single"
                    //     v => (OrganizationType)Enum.Parse(typeof(OrganizationType), v, true)
                    // );
                entity.Property(e => e.ApplicationType)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasDefaultValue(ApplicationType.Empty)
                    .HasConversion(
                        v => v == ApplicationType.Empty ? "" : v.ToString().ToLower(),
                        v => string.IsNullOrEmpty(v) ? ApplicationType.Empty : (ApplicationType)Enum.Parse(typeof(ApplicationType), v, true)
                    );
                entity.Property(e => e.LicenseType)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (LicenseType)Enum.Parse(typeof(LicenseType), v, true)
                    );

                entity.Property(e => e.ApplicationStatus)
                    .IsRequired()
                    .HasDefaultValue(1);   
            });

            modelBuilder.Entity<MstApplication>()
                .HasQueryFilter(m => m.ApplicationStatus != 0);
            modelBuilder.Entity<MstIntegration>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstAccessControl>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstAccessCctv>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstArea>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstBleReader>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstBrand>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstDepartment>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstDistrict>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstFloor>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstMember>()
                .HasQueryFilter(m => m.Status != 0);
            modelBuilder.Entity<MstOrganization>()
                .HasQueryFilter(m => m.Status != 0);

            // MstIntegration
            modelBuilder.Entity<MstIntegration>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.BrandId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.IntegrationType)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (IntegrationType)Enum.Parse(typeof(IntegrationType), v, true)
                    );
                entity.Property(e => e.ApiTypeAuth)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v == ApiTypeAuth.ApiKey ? "apikey" : v.ToString().ToLower(),
                        v => v == "apikey" ? ApiTypeAuth.ApiKey : (ApiTypeAuth)Enum.Parse(typeof(ApiTypeAuth), v, true)
                    );

                entity.HasOne(m => m.Application)
                    .WithMany(a => a.Integrations)
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Brand)
                    .WithMany()
                    .HasForeignKey(m => m.BrandId)
                    .OnDelete(DeleteBehavior.NoAction);
                    
                entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);   
            });

            // MstAccessCctv
            modelBuilder.Entity<MstAccessCctv>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.IntegrationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();

                entity.HasOne(m => m.Integration)
                    .WithMany()
                    .HasForeignKey(m => m.IntegrationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
                
                entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1); 
            });

            // MstAccessControl
            modelBuilder.Entity<MstAccessControl>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.IntegrationId).HasMaxLength(36).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.Integration)
                    .WithMany()
                    .HasForeignKey(m => m.IntegrationId)
                    .OnDelete(DeleteBehavior.NoAction);

                 entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);   
            });

            // MstBrand
            modelBuilder.Entity<MstBrand>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
            });

            // MstOrganization
            modelBuilder.Entity<MstOrganization>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);
                
                entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);  
            });

            // MstDepartment
            modelBuilder.Entity<MstDepartment>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);

                 entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);  
            });

            // MstDistrict
            modelBuilder.Entity<MstDistrict>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();

                entity.HasOne(m => m.Application)
                    .WithMany()
                    .HasForeignKey(m => m.ApplicationId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);  
            });

            // MstMember
            modelBuilder.Entity<MstMember>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.OrganizationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.DepartmentId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.DistrictId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.Gender)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (Gender)Enum.Parse(typeof(Gender), v, true)
                    );
                entity.Property(e => e.StatusEmployee)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v == StatusEmployee.NonActive ? "non-active" : v.ToString().ToLower(),
                        v => v == "non-active" ? StatusEmployee.NonActive : (StatusEmployee)Enum.Parse(typeof(StatusEmployee), v, true)
                    );

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
                
                entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);  

                entity.HasIndex(m => m.PersonId);
                entity.HasIndex(m => m.Email);
            });

            // MstFloor
            modelBuilder.Entity<MstFloor>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.BuildingId).HasMaxLength(255);
                entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);   
            });

            // MstArea
            modelBuilder.Entity<MstArea>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.FloorId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.RestrictedStatus)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v == RestrictedStatus.NonRestrict ? "non-restrict" : v.ToString().ToLower(),
                        v => v == "non-restrict" ? RestrictedStatus.NonRestrict : (RestrictedStatus)Enum.Parse(typeof(RestrictedStatus), v, true)
                    );

                entity.HasOne(m => m.Floor)
                    .WithMany()
                    .HasForeignKey(m => m.FloorId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);   
            });

            // Visitor
            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ApplicationId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.Gender)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (Gender)Enum.Parse(typeof(Gender), v, true)
                    );
                entity.Property(e => e.Status)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v.ToString().ToLower(),
                        v => (VisitorStatus)Enum.Parse(typeof(VisitorStatus), v, true)
                    );

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
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.AreaId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.VisitorId).HasMaxLength(36).IsRequired();

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
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.BrandId).HasMaxLength(36).IsRequired();

                entity.HasOne(m => m.Brand)
                    .WithMany()
                    .HasForeignKey(m => m.BrandId)
                    .OnDelete(DeleteBehavior.NoAction);
                    
                entity.Property(m => m.Status)
                    .IsRequired()
                    .HasDefaultValue(1);
                  
            });

            // TrackingTransaction
            modelBuilder.Entity<TrackingTransaction>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(36).IsRequired();
                entity.Property(e => e.ReaderId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.AreaId).HasMaxLength(36).IsRequired();
                entity.Property(e => e.AlarmStatus)
                    .HasColumnType("nvarchar(255)")
                    .IsRequired()
                    .HasConversion(
                        v => v == AlarmStatus.NonActive ? "non-active" : v.ToString().ToLower(),
                        v => v == "non-active" ? AlarmStatus.NonActive : (AlarmStatus)Enum.Parse(typeof(AlarmStatus), v, true)
                    );

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