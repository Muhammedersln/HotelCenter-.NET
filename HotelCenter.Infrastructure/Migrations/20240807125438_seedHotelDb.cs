using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelCenter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedHotelDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Sqft", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, "Kraliyet tarzında tasarlanmış lüks ve konforlu bir villa.", "https://placehold.co/600x400", "Kraliyet Villası", 4, 200.0, 550, null },
                    { 2, null, "Özel havuzu ve modern olanaklarıyla premium bir villa deneyimi.", "https://placehold.co/600x401", "Premium Havuzlu Villa", 4, 300.0, 550, null },
                    { 3, null, "Geniş yaşam alanı ve özel havuzuyla lüks bir tatil villası.", "https://placehold.co/600x402", "Lüks Havuzlu Villa", 4, 400.0, 750, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
