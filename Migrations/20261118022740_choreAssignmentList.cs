using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRules.Migrations
{
    /// <inheritdoc />
    public partial class choreAssignmentList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIGidBwK8T5+JOiRhj9EdOEUmHT0Rc5MeCMuo6jwZto925qz4gsKy3AX6EBywApJeg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEA4m5a05SlHuMnQ6Ame4he1oD7TPEEeKdy+kTI/hKLW6pvEiwHLsCfxpWX065vwGUA==");
        }
    }
}
