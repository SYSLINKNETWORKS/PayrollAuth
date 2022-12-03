using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TWP_API_Auth.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cf_Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressPermanent = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateofJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Married = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CNIC = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CNICExpire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NTN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyExpirence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyExpirenceDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyExpirenceFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyExpirenceTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyExpirenceRemarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QualificationInstitute = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QualificationYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QualificationRemarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gratuity = table.Column<bool>(type: "bit", nullable: false),
                    EOBI = table.Column<bool>(type: "bit", nullable: false),
                    EOBIRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EOBIRegistrationNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SESSI = table.Column<bool>(type: "bit", nullable: false),
                    SESSIRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SESSIRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StopPayment = table.Column<bool>(type: "bit", nullable: false),
                    ResignationCheck = table.Column<bool>(type: "bit", nullable: false),
                    ResignationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResignationRemarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModeOfPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalaryAccount = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OverTime = table.Column<bool>(type: "bit", nullable: false),
                    OverTimeHoliday = table.Column<bool>(type: "bit", nullable: false),
                    OverTimeRate = table.Column<double>(type: "float", nullable: false),
                    OvertimeSaturday = table.Column<bool>(type: "bit", nullable: false),
                    OverTimeFactory = table.Column<bool>(type: "bit", nullable: false),
                    LateDeduction = table.Column<bool>(type: "bit", nullable: false),
                    AttendanceAllowance = table.Column<bool>(type: "bit", nullable: false),
                    AttendanceExempt = table.Column<bool>(type: "bit", nullable: false),
                    OverTimeRateCheck = table.Column<bool>(type: "bit", nullable: false),
                    DocumentAuthorize = table.Column<bool>(type: "bit", nullable: false),
                    TemporaryPermanent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverTimeSpecialRate = table.Column<double>(type: "float", nullable: false),
                    IncomeTax = table.Column<bool>(type: "bit", nullable: false),
                    ProvisionPeriod = table.Column<int>(type: "int", nullable: false),
                    DateofParmanent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmergencyContactOne = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmergencyContactTwo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TakafulRate = table.Column<double>(type: "float", nullable: false),
                    OfficeWorker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceOne = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceCNICOne = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ReferenceAddressOne = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceContactOne = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferenceTwo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceCNICTwo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ReferenceAddressTwo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ReferenceContactTwo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cf_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Web = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    HeadOffice = table.Column<bool>(type: "bit", nullable: false),
                    LogoHeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoFooter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllBranchCheck = table.Column<bool>(type: "bit", nullable: false),
                    PermissionDateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermissionDateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYear",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StartMonth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StartYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDay = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EndMonth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EndYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialYear_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialYear_Users_UserIdDelete",
                        column: x => x.UserIdDelete,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialYear_Users_UserIdInsert",
                        column: x => x.UserIdInsert,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialYear_Users_UserIdUpdate",
                        column: x => x.UserIdUpdate,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TableErrorLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableErrorLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableErrorLog_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuCategory_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuCategory_Users_UserIdDelete",
                        column: x => x.UserIdDelete,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuCategory_Users_UserIdInsert",
                        column: x => x.UserIdInsert,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuCategory_Users_UserIdUpdate",
                        column: x => x.UserIdUpdate,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuModule_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuModule_Users_UserIdDelete",
                        column: x => x.UserIdDelete,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuModule_Users_UserIdInsert",
                        column: x => x.UserIdInsert,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuModule_Users_UserIdUpdate",
                        column: x => x.UserIdUpdate,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginAudit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WanIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginAudit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLoginAudit_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLoginAudit_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLoginAudit_FinancialYear_YearId",
                        column: x => x.YearId,
                        principalTable: "FinancialYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLoginAudit_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuSubCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuSubCategory_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuSubCategory_UserMenuCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "UserMenuCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuSubCategory_Users_UserIdDelete",
                        column: x => x.UserIdDelete,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuSubCategory_Users_UserIdInsert",
                        column: x => x.UserIdInsert,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenuSubCategory_Users_UserIdUpdate",
                        column: x => x.UserIdUpdate,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    View = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenu_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenu_UserMenuModule_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "UserMenuModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenu_UserMenuSubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "UserMenuSubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenu_Users_UserIdDelete",
                        column: x => x.UserIdDelete,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenu_Users_UserIdInsert",
                        column: x => x.UserIdInsert,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMenu_Users_UserIdUpdate",
                        column: x => x.UserIdUpdate,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthClaim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menu_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthClaim_UserMenu_Menu_Id",
                        column: x => x.Menu_Id,
                        principalTable: "UserMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthClaim_Users_UserIdDelete",
                        column: x => x.UserIdDelete,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthClaim_Users_UserIdInsert",
                        column: x => x.UserIdInsert,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthClaim_Users_UserIdUpdate",
                        column: x => x.UserIdUpdate,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRolePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    View_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Insert_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Update_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Delete_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Print_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Check_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Approve_Permission = table.Column<bool>(type: "bit", nullable: false),
                    Roles_Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Menu_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UserIdInsert = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserIdDelete = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRolePermission_Roles_Roles_Id",
                        column: x => x.Roles_Id,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRolePermission_UserMenu_Menu_Id",
                        column: x => x.Menu_Id,
                        principalTable: "UserMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRolePermission_Users_UserIdDelete",
                        column: x => x.UserIdDelete,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRolePermission_Users_UserIdInsert",
                        column: x => x.UserIdInsert,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRolePermission_Users_UserIdUpdate",
                        column: x => x.UserIdUpdate,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthClaim_Menu_Id",
                table: "AuthClaim",
                column: "Menu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuthClaim_UserIdDelete",
                table: "AuthClaim",
                column: "UserIdDelete");

            migrationBuilder.CreateIndex(
                name: "IX_AuthClaim_UserIdInsert",
                table: "AuthClaim",
                column: "UserIdInsert");

            migrationBuilder.CreateIndex(
                name: "IX_AuthClaim_UserIdUpdate",
                table: "AuthClaim",
                column: "UserIdUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CompanyId",
                table: "Branch",
                column: "CompanyId");

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

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TableErrorLog_UserId",
                table: "TableErrorLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginAudit_BranchId",
                table: "UserLoginAudit",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginAudit_CompanyId",
                table: "UserLoginAudit",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginAudit_UserId",
                table: "UserLoginAudit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginAudit_YearId",
                table: "UserLoginAudit",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenu_CompanyId",
                table: "UserMenu",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenu_ModuleId",
                table: "UserMenu",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenu_SubCategoryId",
                table: "UserMenu",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenu_UserIdDelete",
                table: "UserMenu",
                column: "UserIdDelete");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenu_UserIdInsert",
                table: "UserMenu",
                column: "UserIdInsert");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenu_UserIdUpdate",
                table: "UserMenu",
                column: "UserIdUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuCategory_CompanyId",
                table: "UserMenuCategory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuCategory_UserIdDelete",
                table: "UserMenuCategory",
                column: "UserIdDelete");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuCategory_UserIdInsert",
                table: "UserMenuCategory",
                column: "UserIdInsert");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuCategory_UserIdUpdate",
                table: "UserMenuCategory",
                column: "UserIdUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuModule_CompanyId",
                table: "UserMenuModule",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuModule_UserIdDelete",
                table: "UserMenuModule",
                column: "UserIdDelete");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuModule_UserIdInsert",
                table: "UserMenuModule",
                column: "UserIdInsert");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuModule_UserIdUpdate",
                table: "UserMenuModule",
                column: "UserIdUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuSubCategory_CategoryId",
                table: "UserMenuSubCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuSubCategory_CompanyId",
                table: "UserMenuSubCategory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuSubCategory_UserIdDelete",
                table: "UserMenuSubCategory",
                column: "UserIdDelete");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuSubCategory_UserIdInsert",
                table: "UserMenuSubCategory",
                column: "UserIdInsert");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuSubCategory_UserIdUpdate",
                table: "UserMenuSubCategory",
                column: "UserIdUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermission_Menu_Id",
                table: "UserRolePermission",
                column: "Menu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermission_Roles_Id",
                table: "UserRolePermission",
                column: "Roles_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermission_UserIdDelete",
                table: "UserRolePermission",
                column: "UserIdDelete");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermission_UserIdInsert",
                table: "UserRolePermission",
                column: "UserIdInsert");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolePermission_UserIdUpdate",
                table: "UserRolePermission",
                column: "UserIdUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthClaim");

            migrationBuilder.DropTable(
                name: "Cf_Employee");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "TableErrorLog");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLoginAudit");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRolePermission");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "FinancialYear");

            migrationBuilder.DropTable(
                name: "UserMenu");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserMenuModule");

            migrationBuilder.DropTable(
                name: "UserMenuSubCategory");

            migrationBuilder.DropTable(
                name: "UserMenuCategory");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
