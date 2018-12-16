using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizITAPI.Migrations
{
    public partial class AddedDelBeh5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Quizes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Quizes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Quizes");
        }
    }
}
