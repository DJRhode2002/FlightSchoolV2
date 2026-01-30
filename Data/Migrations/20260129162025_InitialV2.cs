using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSchoolV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "LicenseLevel",
                table: "Students",
                newName: "SelectedPackage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedPackage",
                table: "Students",
                newName: "LicenseLevel");

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
