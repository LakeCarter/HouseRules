using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRules.Migrations
{
    /// <inheritdoc />
    public partial class ChoreComplaetionAssigmentList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEA4m5a05SlHuMnQ6Ame4he1oD7TPEEeKdy+kTI/hKLW6pvEiwHLsCfxpWX065vwGUA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMy7bCWjqoZ9GuT3aJIGrtrS7Aa0tznOQE7PxkfxkqSD/tUnpu1eu2QEBiKXhsxqsg==");
        }
    }
}
