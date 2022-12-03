using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Auth.Migrations
{
    public partial class CompanyNTNSTN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdDelete",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserIdInsert",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserIdUpdate",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserIdDelete",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "UserIdInsert",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "UserIdUpdate",
                table: "Branch");

            migrationBuilder.AddColumn<string>(
                name: "NTN",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STN",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameDelete",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameInsert",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameUpdate",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameDelete",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameInsert",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameUpdate",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NTN",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "STN",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserNameDelete",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserNameInsert",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserNameUpdate",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UserNameDelete",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "UserNameInsert",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "UserNameUpdate",
                table: "Branch");

            migrationBuilder.AddColumn<string>(
                name: "UserIdDelete",
                table: "Company",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdInsert",
                table: "Company",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdUpdate",
                table: "Company",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdDelete",
                table: "Branch",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdInsert",
                table: "Branch",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdUpdate",
                table: "Branch",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);
        }
    }
}
