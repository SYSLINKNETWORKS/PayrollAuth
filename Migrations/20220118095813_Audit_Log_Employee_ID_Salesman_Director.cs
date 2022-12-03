using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Auth.Migrations
{
    public partial class Audit_Log_Employee_ID_Salesman_Director : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cf_Employee");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "CkDirector",
                table: "UserLoginAudit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CkSalesman",
                table: "UserLoginAudit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "UserLoginAudit",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CkDirector",
                table: "UserLoginAudit");

            migrationBuilder.DropColumn(
                name: "CkSalesman",
                table: "UserLoginAudit");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "UserLoginAudit");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Cf_Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressPermanent = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AttendanceAllowance = table.Column<bool>(type: "bit", nullable: false),
                    AttendanceExempt = table.Column<bool>(type: "bit", nullable: false),
                    CNIC = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CNICExpire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyExpirence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyExpirenceDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyExpirenceFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyExpirenceRemarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyExpirenceTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateofJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateofParmanent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentAuthorize = table.Column<bool>(type: "bit", nullable: false),
                    EOBI = table.Column<bool>(type: "bit", nullable: false),
                    EOBIRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EOBIRegistrationNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmergencyContactOne = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmergencyContactTwo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gratuity = table.Column<bool>(type: "bit", nullable: false),
                    IncomeTax = table.Column<bool>(type: "bit", nullable: false),
                    LateDeduction = table.Column<bool>(type: "bit", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    Married = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NTN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OfficeWorker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverTime = table.Column<bool>(type: "bit", nullable: false),
                    OverTimeFactory = table.Column<bool>(type: "bit", nullable: false),
                    OverTimeHoliday = table.Column<bool>(type: "bit", nullable: false),
                    OverTimeRate = table.Column<double>(type: "float", nullable: false),
                    OverTimeRateCheck = table.Column<bool>(type: "bit", nullable: false),
                    OverTimeSpecialRate = table.Column<double>(type: "float", nullable: false),
                    OvertimeSaturday = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProvisionPeriod = table.Column<int>(type: "int", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QualificationInstitute = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QualificationRemarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QualificationYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceAddressOne = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceAddressTwo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceCNICOne = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ReferenceCNICTwo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ReferenceContactOne = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferenceContactTwo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferenceOne = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceTwo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ResignationCheck = table.Column<bool>(type: "bit", nullable: false),
                    ResignationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResignationRemarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SESSI = table.Column<bool>(type: "bit", nullable: false),
                    SESSIRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SESSIRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalaryAccount = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StopPayment = table.Column<bool>(type: "bit", nullable: false),
                    TakafulRate = table.Column<double>(type: "float", nullable: false),
                    TemporaryPermanent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cf_Employee", x => x.Id);
                });
        }
    }
}
