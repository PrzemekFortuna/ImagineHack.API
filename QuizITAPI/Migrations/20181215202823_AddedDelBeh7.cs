using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizITAPI.Migrations
{
    public partial class AddedDelBeh7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomUsers_Rooms_RoomId",
                table: "RoomUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUsers_Rooms_RoomId",
                table: "RoomUsers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomUsers_Rooms_RoomId",
                table: "RoomUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUsers_Rooms_RoomId",
                table: "RoomUsers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
