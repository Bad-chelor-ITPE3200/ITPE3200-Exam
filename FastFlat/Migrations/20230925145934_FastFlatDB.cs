using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFlat.Migrations
{
    /// <inheritdoc />
    public partial class FastFlatDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_LocationModel_LocationID",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Rentals",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_LocationModel_LocationID",
                table: "Rentals",
                column: "LocationID",
                principalTable: "LocationModel",
                principalColumn: "LocationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_LocationModel_LocationID",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Rentals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_LocationModel_LocationID",
                table: "Rentals",
                column: "LocationID",
                principalTable: "LocationModel",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
