using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistrationSite.Data.Migrations
{
    public partial class ChangedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_DriverId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "licenseNumber",
                table: "Drivers",
                newName: "LicenseNumber");

            migrationBuilder.AddColumn<string>(
                name: "VINNumber",
                table: "Vehicles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Drivers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverPhoto",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_DriverId",
                table: "Vehicles",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_DriverId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VINNumber",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DriverPhoto",
                table: "Drivers");

            migrationBuilder.RenameColumn(
                name: "LicenseNumber",
                table: "Drivers",
                newName: "licenseNumber");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Vehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_DriverId",
                table: "Vehicles",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
