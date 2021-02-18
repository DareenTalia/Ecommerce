using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEcommerce.Migrations
{
    public partial class UserTableAddColumnPasswordNotHashed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "age",
                table: "AspNetUsers",
                newName: "Age");

            migrationBuilder.AddColumn<string>(
                name: "MyPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyPassword",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "AspNetUsers",
                newName: "age");
        }
    }
}
