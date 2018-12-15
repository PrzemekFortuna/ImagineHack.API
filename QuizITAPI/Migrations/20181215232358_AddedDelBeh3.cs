using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizITAPI.Migrations
{
    public partial class AddedDelBeh3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxUsersCount",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rooms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxUsersCount",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Rooms");
        }
    }
}
