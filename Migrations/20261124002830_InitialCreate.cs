using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRules.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFFdUCEHP1rurNmKHJEaxhPM1g+57QKDplj/6AdK1LTFeqtIz7QR7E3zzWMu7kUtvw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIGidBwK8T5+JOiRhj9EdOEUmHT0Rc5MeCMuo6jwZto925qz4gsKy3AX6EBywApJeg==");
        }
    }
}
