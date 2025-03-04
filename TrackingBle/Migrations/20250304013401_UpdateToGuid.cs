using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingBle.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MstApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrganizationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationExpired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HostPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HostEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HostAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationCustomName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationCustomDomain = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationCustomPort = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LicenseCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LicenseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MstBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MstFloors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<long>(type: "bigint", nullable: false),
                    FloorImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PixelX = table.Column<long>(type: "bigint", nullable: false),
                    PixelY = table.Column<long>(type: "bigint", nullable: false),
                    FloorX = table.Column<long>(type: "bigint", nullable: false),
                    FloorY = table.Column<long>(type: "bigint", nullable: false),
                    MeterPerPx = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EngineFloorId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstFloors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MstDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DepartmentHost = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstDepartments_MstApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MstApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MstDistricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DistrictHost = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstDistricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstDistricts_MstApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MstApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MstIntegrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IntegrationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiTypeAuth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiAuthUsername = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiAuthPasswd = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiKeyField = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiKeyValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstIntegrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstIntegrations_MstApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MstApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MstOrganizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrganizationHost = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstOrganizations_MstApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MstApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BleCardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadFr = table.Column<int>(type: "int", nullable: false),
                    UploadFrError = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorArrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PortalKey = table.Column<long>(type: "bigint", nullable: false),
                    TimestampPreRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampCheckedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampCheckedOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampDeny = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampBlocked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampUnblocked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckinBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CheckoutBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DenyBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BlockBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UnblockBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReasonDeny = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReasonBlock = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReasonUnblock = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitors_MstApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MstApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MstBleReaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Mac = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LocationX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocationY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocationPxX = table.Column<long>(type: "bigint", nullable: false),
                    LocationPxY = table.Column<long>(type: "bigint", nullable: false),
                    EngineReaderId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstBleReaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstBleReaders_MstBrands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "MstBrands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MstAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AreaShape = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorArea = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RestrictedStatus = table.Column<int>(type: "int", nullable: false),
                    EngineAreaId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WideArea = table.Column<long>(type: "bigint", nullable: false),
                    PositionPxX = table.Column<long>(type: "bigint", nullable: false),
                    PositionPxY = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MstFloorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstAreas_MstFloors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "MstFloors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstAreas_MstFloors_MstFloorId",
                        column: x => x.MstFloorId,
                        principalTable: "MstFloors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MstAccessCctvs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rtsp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstAccessCctvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstAccessCctvs_MstApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MstApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstAccessCctvs_MstIntegrations_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "MstIntegrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MstAccessControls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControllerBrandId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DoorId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Raw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstAccessControls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstAccessControls_MstApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MstApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstAccessControls_MstIntegrations_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "MstIntegrations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MstMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BleCardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadFr = table.Column<int>(type: "int", nullable: false),
                    UploadFrError = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeadMember1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HeadMember2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    StatusEmployee = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MstDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MstDistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MstOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MstMembers_MstApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "MstApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstMembers_MstDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "MstDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstMembers_MstDepartments_MstDepartmentId",
                        column: x => x.MstDepartmentId,
                        principalTable: "MstDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstMembers_MstDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "MstDistricts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstMembers_MstDistricts_MstDistrictId",
                        column: x => x.MstDistrictId,
                        principalTable: "MstDistricts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstMembers_MstOrganizations_MstOrganizationId",
                        column: x => x.MstOrganizationId,
                        principalTable: "MstOrganizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MstMembers_MstOrganizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "MstOrganizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrackingTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    TransTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReaderId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    CardId = table.Column<long>(type: "bigint", nullable: false),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    CoordinateX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoordinateY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoordinatePxX = table.Column<long>(type: "bigint", nullable: false),
                    CoordinatePxY = table.Column<long>(type: "bigint", nullable: false),
                    AlarmStatus = table.Column<int>(type: "int", nullable: false),
                    Battery = table.Column<long>(type: "bigint", nullable: false),
                    MstAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MstBleReaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackingTransactions_MstAreas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "MstAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackingTransactions_MstAreas_MstAreaId",
                        column: x => x.MstAreaId,
                        principalTable: "MstAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackingTransactions_MstBleReaders_MstBleReaderId",
                        column: x => x.MstBleReaderId,
                        principalTable: "MstBleReaders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackingTransactions_MstBleReaders_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "MstBleReaders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VisitorBlacklistAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    VisitorId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 255, nullable: false),
                    MstAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VisitorId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorBlacklistAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitorBlacklistAreas_MstAreas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "MstAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisitorBlacklistAreas_MstAreas_MstAreaId",
                        column: x => x.MstAreaId,
                        principalTable: "MstAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisitorBlacklistAreas_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VisitorBlacklistAreas_Visitors_VisitorId1",
                        column: x => x.VisitorId1,
                        principalTable: "Visitors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MstAccessCctvs_ApplicationId",
                table: "MstAccessCctvs",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstAccessCctvs_IntegrationId",
                table: "MstAccessCctvs",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstAccessControls_ApplicationId",
                table: "MstAccessControls",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstAccessControls_IntegrationId",
                table: "MstAccessControls",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstAreas_FloorId",
                table: "MstAreas",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_MstAreas_MstFloorId",
                table: "MstAreas",
                column: "MstFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_MstBleReaders_BrandId",
                table: "MstBleReaders",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_MstDepartments_ApplicationId",
                table: "MstDepartments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstDistricts_ApplicationId",
                table: "MstDistricts",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstIntegrations_ApplicationId",
                table: "MstIntegrations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_ApplicationId",
                table: "MstMembers",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_DepartmentId",
                table: "MstMembers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_DistrictId",
                table: "MstMembers",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_Email",
                table: "MstMembers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_MstDepartmentId",
                table: "MstMembers",
                column: "MstDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_MstDistrictId",
                table: "MstMembers",
                column: "MstDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_MstOrganizationId",
                table: "MstMembers",
                column: "MstOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_OrganizationId",
                table: "MstMembers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstMembers_PersonId",
                table: "MstMembers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MstOrganizations_ApplicationId",
                table: "MstOrganizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingTransactions_AreaId",
                table: "TrackingTransactions",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingTransactions_MstAreaId",
                table: "TrackingTransactions",
                column: "MstAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingTransactions_MstBleReaderId",
                table: "TrackingTransactions",
                column: "MstBleReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingTransactions_ReaderId",
                table: "TrackingTransactions",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorBlacklistAreas_AreaId",
                table: "VisitorBlacklistAreas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorBlacklistAreas_MstAreaId",
                table: "VisitorBlacklistAreas",
                column: "MstAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorBlacklistAreas_VisitorId",
                table: "VisitorBlacklistAreas",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorBlacklistAreas_VisitorId1",
                table: "VisitorBlacklistAreas",
                column: "VisitorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_ApplicationId",
                table: "Visitors",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_Email",
                table: "Visitors",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_PersonId",
                table: "Visitors",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MstAccessCctvs");

            migrationBuilder.DropTable(
                name: "MstAccessControls");

            migrationBuilder.DropTable(
                name: "MstMembers");

            migrationBuilder.DropTable(
                name: "TrackingTransactions");

            migrationBuilder.DropTable(
                name: "VisitorBlacklistAreas");

            migrationBuilder.DropTable(
                name: "MstIntegrations");

            migrationBuilder.DropTable(
                name: "MstDepartments");

            migrationBuilder.DropTable(
                name: "MstDistricts");

            migrationBuilder.DropTable(
                name: "MstOrganizations");

            migrationBuilder.DropTable(
                name: "MstBleReaders");

            migrationBuilder.DropTable(
                name: "MstAreas");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "MstBrands");

            migrationBuilder.DropTable(
                name: "MstFloors");

            migrationBuilder.DropTable(
                name: "MstApplications");
        }
    }
}
