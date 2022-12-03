using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TWP_API_Auth.Models;

// insert into roles(id,Name,NormalizedName) values(NewID(),'Admin','Admin')
// insert into roles(id,Name,NormalizedName) values(NewID(),'Guest','Guest')
// insert into roles(id,Name,NormalizedName) values(NewID(),'Member','Member')
//insert into usergroup(id,Name,Type,Active,Action,Userid,MakerDate) values(NewID(),'Admin','S',1,'A',1,curdate());

// insert into roles(id,Name,NormalizedName) values(UUID(),'Admin','Admin');
// insert into roles(id,Name,NormalizedName) values(UUID(),'Guest','Guest');
// insert into roles(id,Name,NormalizedName) values(UUID(),'Member','Member');
//insert into usergroup(id,Name,Type,Active,Action,Userid,MakerDate) values(UUID(),'Admin','S',1,'A',1,curdate());
//    "Connection": "server=www.syslinknetworkiot.com; port=3306; database=abdulsattar_Auth; user=MSAuth; password=1234$Auth; Persist Security Info=False; Connect Timeout=300"

//    "Connection": "server=demo.syslinknetwork.com; port=3306; database=AuthDB; user=msauth; password=1234$Test; Persist Security Info=False; Connect Timeout=300"
//    "Connection": "server=localhost; port=3306; database=AuthDB; user=root; password=1234$Test; Persist Security Info=False; Connect Timeout=300"
// http://demo.syslinknetwork.com:83/TWP_API_Auth/swagger/index.html

namespace TWP_API_Auth.App_Data
{
    public partial class DataContext : IdentityDbContext<ApplicationUser>
    {
        //DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<AuthClaim> AuthClaims { get; set; }
        public virtual DbSet<UserLoginAudit> UserLoginAudits { get; set; }
        public virtual DbSet<UserMenuModule> UserMenuModules { get; set; }
        public virtual DbSet<UserMenuCategory> UserMenuCategories { get; set; }
        public virtual DbSet<UserMenuSubCategory> UserMenuSubCategories { get; set; }
        public virtual DbSet<UserMenu> UserMenus { get; set; }
        public virtual DbSet<UserRolePermission> UserRolePermissions { get; set; }
        public virtual DbSet<UserItemCategory> UserItemCategories { get; set; }

        public virtual DbSet<TableErrorLog> TableErrorLogs { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<FinancialYear> FinancialYears { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }

            }

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        }
    }
}