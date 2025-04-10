using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingBle.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "alarm_status",
                table: "alarm_record_tracking",
                newName: "alarm_record_status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "alarm_record_status",
                table: "alarm_record_tracking",
                newName: "alarm_status");
        }
    }
}
