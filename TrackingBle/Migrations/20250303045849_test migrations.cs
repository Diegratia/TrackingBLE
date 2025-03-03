using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingBle.Migrations
{
    /// <inheritdoc />
    public partial class testmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MstApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false),
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
                name: "MstIntegrations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Generate = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IntegrationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiTypeAuth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiAuthUsername = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiAuthPasswd = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiKeyField = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApiKeyValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
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
                name: "MstAccessCctvs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Generate = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rtsp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntegrationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_MstAccessCctvs_ApplicationId",
                table: "MstAccessCctvs",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstAccessCctvs_IntegrationId",
                table: "MstAccessCctvs",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_MstIntegrations_ApplicationId",
                table: "MstIntegrations",
                column: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MstAccessCctvs");

            migrationBuilder.DropTable(
                name: "MstIntegrations");

            migrationBuilder.DropTable(
                name: "MstApplications");
        }
    }
}
