using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Data.Migrations
{
    public partial class UserB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_UserId1",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_UserId1",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId1",
                table: "Clients",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_UserId1",
                table: "Clients",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
