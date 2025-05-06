using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Vehicle.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class fluentapi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleProducts_VehicleCategories_VehicleCategoryCategoryId",
                table: "VehicleProducts");

            migrationBuilder.DropIndex(
                name: "IX_VehicleProducts_VehicleCategoryCategoryId",
                table: "VehicleProducts");

            migrationBuilder.DropColumn(
                name: "VehicleCategoryCategoryId",
                table: "VehicleProducts");

            migrationBuilder.AddUniqueConstraint(
                name: "unique_ProductName",
                table: "VehicleProducts",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleProducts_CategoryId",
                table: "VehicleProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleProduct_VehicleCategory",
                table: "VehicleProducts",
                column: "CategoryId",
                principalTable: "VehicleCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleProduct_VehicleCategory",
                table: "VehicleProducts");

            migrationBuilder.DropUniqueConstraint(
                name: "unique_ProductName",
                table: "VehicleProducts");

            migrationBuilder.DropIndex(
                name: "IX_VehicleProducts_CategoryId",
                table: "VehicleProducts");

            migrationBuilder.AddColumn<byte>(
                name: "VehicleCategoryCategoryId",
                table: "VehicleProducts",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleProducts_VehicleCategoryCategoryId",
                table: "VehicleProducts",
                column: "VehicleCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleProducts_VehicleCategories_VehicleCategoryCategoryId",
                table: "VehicleProducts",
                column: "VehicleCategoryCategoryId",
                principalTable: "VehicleCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
