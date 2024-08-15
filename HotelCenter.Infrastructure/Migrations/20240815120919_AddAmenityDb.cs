using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelCenter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAmenityDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenities_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Description", "HotelId", "Name" },
                values: new object[,]
                {
                    { 1, null, 1, "Özel Havuz" },
                    { 2, null, 1, "Mikrodalga Fırın" },
                    { 3, null, 1, "Özel Balkon" },
                    { 4, null, 1, "1 king yatak ve 1 çekyat" },
                    { 5, null, 2, "Özel Dalma Havuzu" },
                    { 6, null, 2, "Mikrodalga ve Mini Buzdolabı" },
                    { 7, null, 2, "Özel Balkon" },
                    { 8, null, 2, "king yatak veya 2 çift kişilik yatak" },
                    { 9, null, 3, "Özel Havuz" },
                    { 10, null, 3, "Jakuzi" },
                    { 11, null, 3, "Özel Balkon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_HotelId",
                table: "Amenities",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities");
        }
    }
}
