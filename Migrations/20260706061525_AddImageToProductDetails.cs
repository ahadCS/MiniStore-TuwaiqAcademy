using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mini_store.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToProductDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProductDetails");
        }
    }
}
