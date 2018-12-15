using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizITAPI.Migrations
{
    public partial class AddedDelBeh1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "EMail", "Password", "Token" },
                values: new object[] { 1, "testEmail@gmail.com", "Password", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");
        }
    }
}
