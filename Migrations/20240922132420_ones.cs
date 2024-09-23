using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SercPag_sorting.Migrations
{
    /// <inheritdoc />
    public partial class ones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "001");

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { "002", "ade@xyz.com", "adeyemi" },
                    { "003", "bala@xyz.com", "bala" },
                    { "004", "adewumi@xyz.com", "adewumi" },
                    { "005", "adere@xyz.com", "adere" },
                    { "006", "adebo@xyz.com", "adebo" },
                    { "007", "debo@xyz.com", "debo" },
                    { "008", "bumi@gmail.com", "bumi" },
                    { "009", "soji@gmail.com", "soji" },
                    { "010", "taya@gmail.com", "taya" },
                    { "011", "tomi@gmail.com", "tomi" },
                    { "012", "dami@gmail.com", "dami" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "002");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "003");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "004");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "005");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "006");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "007");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "008");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "009");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "010");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "011");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "Id",
                keyValue: "012");

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { "001", "ade@xyz.com", "adeyemi" });
        }
    }
}
