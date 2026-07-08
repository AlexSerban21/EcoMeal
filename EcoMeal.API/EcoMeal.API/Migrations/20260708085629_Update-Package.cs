using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartRidicare",
                table: "Packages",
                newName: "StartPickup");

            migrationBuilder.RenameColumn(
                name: "EndRidicare",
                table: "Packages",
                newName: "EndPickup");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Packages",
                type: "float(10)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "StartPickup",
                table: "Packages",
                newName: "StartRidicare");

            migrationBuilder.RenameColumn(
                name: "EndPickup",
                table: "Packages",
                newName: "EndRidicare");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Packages",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(10)",
                oldPrecision: 10,
                oldScale: 2);
        }
    }
}
