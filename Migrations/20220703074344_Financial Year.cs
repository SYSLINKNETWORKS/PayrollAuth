using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Auth.Migrations
{
    public partial class FinancialYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialYear_Company_CompanyId",
                table: "FinancialYear");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialYear_Users_UserIdDelete",
                table: "FinancialYear");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialYear_Users_UserIdInsert",
                table: "FinancialYear");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialYear_Users_UserIdUpdate",
                table: "FinancialYear");

            migrationBuilder.DropIndex(
                name: "IX_FinancialYear_CompanyId",
                table: "FinancialYear");

            migrationBuilder.DropIndex(
                name: "IX_FinancialYear_UserIdDelete",
                table: "FinancialYear");

            migrationBuilder.DropIndex(
                name: "IX_FinancialYear_UserIdInsert",
                table: "FinancialYear");

            migrationBuilder.DropIndex(
                name: "IX_FinancialYear_UserIdUpdate",
                table: "FinancialYear");

            migrationBuilder.DropColumn(
                name: "UserIdDelete",
                table: "FinancialYear");

            migrationBuilder.DropColumn(
                name: "UserIdInsert",
                table: "FinancialYear");

            migrationBuilder.DropColumn(
                name: "UserIdUpdate",
                table: "FinancialYear");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "FinancialYear",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "FinancialYear",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<int>(
                name: "StartYear",
                table: "FinancialYear",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "StartMonth",
                table: "FinancialYear",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "StartDay",
                table: "FinancialYear",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "FinancialYear",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "EndYear",
                table: "FinancialYear",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "EndMonth",
                table: "FinancialYear",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "EndDay",
                table: "FinancialYear",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeleteDate",
                table: "FinancialYear",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "UserNameDelete",
                table: "FinancialYear",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameInsert",
                table: "FinancialYear",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNameUpdate",
                table: "FinancialYear",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserNameDelete",
                table: "FinancialYear");

            migrationBuilder.DropColumn(
                name: "UserNameInsert",
                table: "FinancialYear");

            migrationBuilder.DropColumn(
                name: "UserNameUpdate",
                table: "FinancialYear");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "FinancialYear",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "FinancialYear",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StartYear",
                table: "FinancialYear",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StartMonth",
                table: "FinancialYear",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StartDay",
                table: "FinancialYear",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "FinancialYear",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndYear",
                table: "FinancialYear",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EndMonth",
                table: "FinancialYear",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EndDay",
                table: "FinancialYear",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeleteDate",
                table: "FinancialYear",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdDelete",
                table: "FinancialYear",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdInsert",
                table: "FinancialYear",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdUpdate",
                table: "FinancialYear",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYear_CompanyId",
                table: "FinancialYear",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYear_UserIdDelete",
                table: "FinancialYear",
                column: "UserIdDelete");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYear_UserIdInsert",
                table: "FinancialYear",
                column: "UserIdInsert");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYear_UserIdUpdate",
                table: "FinancialYear",
                column: "UserIdUpdate");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialYear_Company_CompanyId",
                table: "FinancialYear",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialYear_Users_UserIdDelete",
                table: "FinancialYear",
                column: "UserIdDelete",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialYear_Users_UserIdInsert",
                table: "FinancialYear",
                column: "UserIdInsert",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialYear_Users_UserIdUpdate",
                table: "FinancialYear",
                column: "UserIdUpdate",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
