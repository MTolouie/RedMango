using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedMango_DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AlteredCartDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_MenuItems_BookId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_BookId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "CartDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_MenuItemId",
                table: "CartDetails",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_MenuItems_MenuItemId",
                table: "CartDetails",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_MenuItems_MenuItemId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_MenuItemId",
                table: "CartDetails");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "CartDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_BookId",
                table: "CartDetails",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_MenuItems_BookId",
                table: "CartDetails",
                column: "BookId",
                principalTable: "MenuItems",
                principalColumn: "Id");
        }
    }
}
