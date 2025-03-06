using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingBle.Migrations
{
    /// <inheritdoc />
    public partial class changeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mst_application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrganizationType = table.Column<string>(type: "nvarchar(255)", nullable: false, defaultValue: "Single"),
                    OrganizationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationType = table.Column<string>(type: "nvarchar(255)", nullable: false, defaultValue: ""),
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
                    LicenseType = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_brand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_floor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
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
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_floor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DepartmentHost = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_department_mst_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_department_mst_application_MstApplicationId",
                        column: x => x.MstApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_district",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DistrictHost = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_district", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_district_mst_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_district_mst_application_MstApplicationId",
                        column: x => x.MstApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrganizationHost = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_organization_mst_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_organization_mst_application_MstApplicationId",
                        column: x => x.MstApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "visitor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BleCardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadFr = table.Column<int>(type: "int", nullable: false),
                    UploadFrError = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
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
                    Status = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    MstApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_visitor_mst_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_visitor_mst_application_MstApplicationId",
                        column: x => x.MstApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_ble_reader",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
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
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_ble_reader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_ble_reader_mst_brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "mst_brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_ble_reader_mst_brand_MstBrandId",
                        column: x => x.MstBrandId,
                        principalTable: "mst_brand",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_integration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    IntegrationType = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    ApiTypeAuth = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    ApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiAuthUsername = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiAuthPasswd = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiKeyField = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiKeyValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_integration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_integration_mst_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_integration_mst_brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "mst_brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_integration_mst_brand_MstBrandId",
                        column: x => x.MstBrandId,
                        principalTable: "mst_brand",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "floorplan_masked_area",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorplanId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AreaShape = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorArea = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RestrictedStatus = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    EngineAreaId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WideArea = table.Column<long>(type: "bigint", nullable: false),
                    PositionPxX = table.Column<long>(type: "bigint", nullable: false),
                    PositionPxY = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstFloorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_floorplan_masked_area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_floorplan_masked_area_mst_floor_FloorId",
                        column: x => x.FloorId,
                        principalTable: "mst_floor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_floorplan_masked_area_mst_floor_MstFloorId",
                        column: x => x.MstFloorId,
                        principalTable: "mst_floor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_member",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BleCardNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadFr = table.Column<int>(type: "int", nullable: false),
                    UploadFrError = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeadMember1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HeadMember2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    StatusEmployee = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MstDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MstDistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MstOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_member_mst_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_member_mst_application_MstApplicationId",
                        column: x => x.MstApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_member_mst_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "mst_department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_member_mst_department_MstDepartmentId",
                        column: x => x.MstDepartmentId,
                        principalTable: "mst_department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_member_mst_district_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "mst_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_member_mst_district_MstDistrictId",
                        column: x => x.MstDistrictId,
                        principalTable: "mst_district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_member_mst_organization_MstOrganizationId",
                        column: x => x.MstOrganizationId,
                        principalTable: "mst_organization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_member_mst_organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "mst_organization",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_access_cctv",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rtsp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_access_cctv", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_access_cctv_mst_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_access_cctv_mst_application_MstApplicationId",
                        column: x => x.MstApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_access_cctv_mst_integration_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "mst_integration",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_access_control",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControllerBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DoorId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Raw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntegrationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MstApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_access_control", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_access_control_mst_application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_access_control_mst_application_MstApplicationId",
                        column: x => x.MstApplicationId,
                        principalTable: "mst_application",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_access_control_mst_brand_ControllerBrandId",
                        column: x => x.ControllerBrandId,
                        principalTable: "mst_brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mst_access_control_mst_integration_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "mst_integration",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tracking_transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    TransTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReaderId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    CardId = table.Column<long>(type: "bigint", nullable: false),
                    FloorplanId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    CoordinateX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoordinateY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoordinatePxX = table.Column<long>(type: "bigint", nullable: false),
                    CoordinatePxY = table.Column<long>(type: "bigint", nullable: false),
                    AlarmStatus = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Battery = table.Column<long>(type: "bigint", nullable: false),
                    FloorplanMaskedAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MstBleReaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tracking_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tracking_transaction_floorplan_masked_area_FloorplanId",
                        column: x => x.FloorplanId,
                        principalTable: "floorplan_masked_area",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tracking_transaction_floorplan_masked_area_FloorplanMaskedAreaId",
                        column: x => x.FloorplanMaskedAreaId,
                        principalTable: "floorplan_masked_area",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tracking_transaction_mst_ble_reader_MstBleReaderId",
                        column: x => x.MstBleReaderId,
                        principalTable: "mst_ble_reader",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tracking_transaction_mst_ble_reader_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "mst_ble_reader",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "visitor_blacklist_area",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorplanId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    VisitorId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    FloorplanMaskedAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VisitorId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visitor_blacklist_area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_visitor_blacklist_area_floorplan_masked_area_FloorplanId",
                        column: x => x.FloorplanId,
                        principalTable: "floorplan_masked_area",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_visitor_blacklist_area_floorplan_masked_area_FloorplanMaskedAreaId",
                        column: x => x.FloorplanMaskedAreaId,
                        principalTable: "floorplan_masked_area",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_visitor_blacklist_area_visitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "visitor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_visitor_blacklist_area_visitor_VisitorId1",
                        column: x => x.VisitorId1,
                        principalTable: "visitor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_floorplan_masked_area_FloorId",
                table: "floorplan_masked_area",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_floorplan_masked_area_MstFloorId",
                table: "floorplan_masked_area",
                column: "MstFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_access_cctv_ApplicationId",
                table: "mst_access_cctv",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_access_cctv_IntegrationId",
                table: "mst_access_cctv",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_access_cctv_MstApplicationId",
                table: "mst_access_cctv",
                column: "MstApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_access_control_ApplicationId",
                table: "mst_access_control",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_access_control_ControllerBrandId",
                table: "mst_access_control",
                column: "ControllerBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_access_control_IntegrationId",
                table: "mst_access_control",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_access_control_MstApplicationId",
                table: "mst_access_control",
                column: "MstApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_ble_reader_BrandId",
                table: "mst_ble_reader",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_ble_reader_MstBrandId",
                table: "mst_ble_reader",
                column: "MstBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_ApplicationId",
                table: "mst_department",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_MstApplicationId",
                table: "mst_department",
                column: "MstApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_district_ApplicationId",
                table: "mst_district",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_district_MstApplicationId",
                table: "mst_district",
                column: "MstApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_integration_ApplicationId",
                table: "mst_integration",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_integration_BrandId",
                table: "mst_integration",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_integration_MstBrandId",
                table: "mst_integration",
                column: "MstBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_ApplicationId",
                table: "mst_member",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_DepartmentId",
                table: "mst_member",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_DistrictId",
                table: "mst_member",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_Email",
                table: "mst_member",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_MstApplicationId",
                table: "mst_member",
                column: "MstApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_MstDepartmentId",
                table: "mst_member",
                column: "MstDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_MstDistrictId",
                table: "mst_member",
                column: "MstDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_MstOrganizationId",
                table: "mst_member",
                column: "MstOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_OrganizationId",
                table: "mst_member",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_member_PersonId",
                table: "mst_member",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_organization_ApplicationId",
                table: "mst_organization",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_organization_MstApplicationId",
                table: "mst_organization",
                column: "MstApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_tracking_transaction_FloorplanId",
                table: "tracking_transaction",
                column: "FloorplanId");

            migrationBuilder.CreateIndex(
                name: "IX_tracking_transaction_FloorplanMaskedAreaId",
                table: "tracking_transaction",
                column: "FloorplanMaskedAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_tracking_transaction_MstBleReaderId",
                table: "tracking_transaction",
                column: "MstBleReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_tracking_transaction_ReaderId",
                table: "tracking_transaction",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_visitor_ApplicationId",
                table: "visitor",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_visitor_Email",
                table: "visitor",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_visitor_MstApplicationId",
                table: "visitor",
                column: "MstApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_visitor_PersonId",
                table: "visitor",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_visitor_blacklist_area_FloorplanId",
                table: "visitor_blacklist_area",
                column: "FloorplanId");

            migrationBuilder.CreateIndex(
                name: "IX_visitor_blacklist_area_FloorplanMaskedAreaId",
                table: "visitor_blacklist_area",
                column: "FloorplanMaskedAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_visitor_blacklist_area_VisitorId",
                table: "visitor_blacklist_area",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_visitor_blacklist_area_VisitorId1",
                table: "visitor_blacklist_area",
                column: "VisitorId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mst_access_cctv");

            migrationBuilder.DropTable(
                name: "mst_access_control");

            migrationBuilder.DropTable(
                name: "mst_member");

            migrationBuilder.DropTable(
                name: "tracking_transaction");

            migrationBuilder.DropTable(
                name: "visitor_blacklist_area");

            migrationBuilder.DropTable(
                name: "mst_integration");

            migrationBuilder.DropTable(
                name: "mst_department");

            migrationBuilder.DropTable(
                name: "mst_district");

            migrationBuilder.DropTable(
                name: "mst_organization");

            migrationBuilder.DropTable(
                name: "mst_ble_reader");

            migrationBuilder.DropTable(
                name: "floorplan_masked_area");

            migrationBuilder.DropTable(
                name: "visitor");

            migrationBuilder.DropTable(
                name: "mst_brand");

            migrationBuilder.DropTable(
                name: "mst_floor");

            migrationBuilder.DropTable(
                name: "mst_application");
        }
    }
}
