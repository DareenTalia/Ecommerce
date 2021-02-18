using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEcommerce.Migrations
{
    public partial class UserTableAddColumnPasswordNotHashed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordNotHashed",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordNotHashed",
                table: "AspNetUsers");
        }
    }
}
