using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Models;

namespace TWP_API_Auth.Data
{
    public class DbInitializer
    {

        private readonly DataContext _context;
        public DbInitializer(DataContext context)
        {
            _context = context;
        }
        public async Task Initialize()
        {
            //Email : support@mmc.biz.pk password : 1234$AbdulSattarIbrahim


            _context.Database.EnsureCreated();

            // Look for any Role.
            if (!_context.Roles.Any())
            {
                var IdentityRoles = new IdentityRole[] {
                    new IdentityRole { Name = "SuperAdmin", NormalizedName = "SuperAdmin" },
                    new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" },
                    new IdentityRole { Name = "User", NormalizedName = "User" }
                };
                foreach (IdentityRole s in IdentityRoles)
                {
                    await _context.AddAsync(s);
                }
                _context.SaveChanges();
            }

            // Look for any Company.
            if (!_context.Companies.Any())
            {
                var Company = new Company[] {
                    new Company { Name = "Teamwork Packages (Pvt) Ltd", ShortName = "TWP", Type =  Enums.ColumnType.S.ToString(), Action = Enums.Operations.A.ToString (), Active = true }
                };
                foreach (Company s in Company)
                {
                    _context.Add(s);
                }
                _context.SaveChanges();
            }

            // Look for any Branch.
            if (!_context.Branches.Any())
            {
                var _Company = _context.Companies.OrderBy(a => a.Id).FirstOrDefault();

                var Branch = new Branch[] {
                    new Branch { Name = "Unit - 1", ShortName = "Unit - 1", CompanyId = _Company.Id, Type =  Enums.ColumnType.S.ToString(), Action = Enums.Operations.A.ToString (), Active = true }
                };
                foreach (Branch s in Branch)
                {
                    await _context.AddAsync(s);
                }
                _context.SaveChanges();
            }
            // Look for any FinancialYear.
            if (!_context.FinancialYears.Any())
            {
                var _Company = _context.Companies.OrderBy(a => a.Id).FirstOrDefault();

                var FinancialYear = new FinancialYear[] {
                    new FinancialYear {
                    StartDate = DateTime.Now,
                    StartMonth = Convert.ToInt32( DateTime.Now.ToString ("MM")),
                    StartDay =Convert.ToInt32( DateTime.Now.ToString ("dd")),
                    StartYear = Convert.ToInt32(DateTime.Now.ToString ("yyyy")),
                    EndDate = DateTime.Now,
                    EndMonth =Convert.ToInt32( DateTime.Now.ToString ("MM")),
                    EndDay = Convert.ToInt32(DateTime.Now.ToString ("dd")),
                    EndYear = Convert.ToInt32(DateTime.Now.ToString ("yyyy")),
                    CompanyId = _Company.Id,
                    Type = Enums.ColumnType.S.ToString(),
                    Active = true,
                    Action = Enums.Operations.A.ToString ()
                    }
                };
                foreach (FinancialYear s in FinancialYear)
                {
                    await _context.AddAsync(s);
                }
                _context.SaveChanges();
            }
            // Look for any Users.
            if (!_context.Users.Any())
            {
                var _Branch = _context.Branches.OrderBy(a => a.Id).FirstOrDefault();
                var _Role = _context.Roles.Where(a => a.Name == "SuperAdmin").FirstOrDefault();

                ApplicationUser _model = new ApplicationUser();
                _model.FirstName = "Abdul";
                _model.LastName = "Sattar";
                _model.PhoneNumber = "00923173331701";
                _model.BranchId = _Branch.Id;
                _model.Email = "abdul.sattar@mmc.biz.pk";
                _model.InsertDate = DateTime.Now;
                _model.Action = Enums.Operations.A.ToString ();
                _model.UserName = "AbdulSattar";
                _model.NormalizedUserName = "AbdulSattar";
                _model.Type = Enums.ColumnType.S.ToString();
                _model.NormalizedEmail = "abdul.sattar@mmc.biz.pk";
                _model.EmailConfirmed = true;
                _model.TwoFactorEnabled = false;
                _model.PasswordHash = "AQAAAAEAACcQAAAAEPPFBWTwPo+ZUNJ7T/WFDFnH5WEY20Z1ULFga7u+zygIVVs3TBBcouhD5nKQIjaiMA==";
                _context.Add(_model);
                _context.SaveChanges();

                IdentityUserRole<String> _IdentityUserRole = new IdentityUserRole<String>();
                _IdentityUserRole.RoleId = _Role.Id;
                _IdentityUserRole.UserId = _model.Id;
                _context.Add(_IdentityUserRole);
                _context.SaveChanges();

            }
            var _UserId = _context.Users.OrderByDescending(r => r.InsertDate).FirstOrDefault();
            //Menu Module
            if (!_context.UserMenuModules.Any())
            {
                var _Company = _context.Companies.OrderBy(a => a.Id).FirstOrDefault();

                var _UserMenuModule = new UserMenuModule[] {
                    new UserMenuModule { Name = "Configuration", Type = Enums.ColumnType.S.ToString(), Icon = "glyphicon glyphicon-file", CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now }
                };

                foreach (UserMenuModule s in _UserMenuModule)
                {
                    await _context.AddAsync(s);
                }
                _context.SaveChanges();
            }

            //Menu Category
            if (!_context.UserMenuCategories.Any())
            {
                var _Company = _context.Companies.OrderBy(a => a.Id).FirstOrDefault();
                var _UserMenuCategory = new UserMenuCategory[] {
                    new UserMenuCategory { Name = "Form", Type = Enums.ColumnType.S.ToString(), CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now },
                    new UserMenuCategory { Name = "Report", Type =  Enums.ColumnType.S.ToString(), CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now }
                };

                foreach (UserMenuCategory s in _UserMenuCategory)
                {
                    await _context.AddAsync(s);
                }
                _context.SaveChanges();
            }

            //Menu Sub-Category
            if (!_context.UserMenuSubCategories.Any())
            {
                var _Company = _context.Companies.OrderBy(a => a.Id).FirstOrDefault();
                var _UserMenuCategory = _context.UserMenuCategories.Where(w => w.Name.ToLower() == "Form".ToLower()).FirstOrDefault();
                var _UserMenuSubCategory = new UserMenuSubCategory[] {
                    new UserMenuSubCategory { Name = "Setup", Type =  Enums.ColumnType.S.ToString(), Icon = "icon-settings", CategoryId = _UserMenuCategory.Id, CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now },
                    new UserMenuSubCategory { Name = "Transaction", Type =  Enums.ColumnType.S.ToString(), Icon = "icon-docs", CategoryId = _UserMenuCategory.Id, CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now },
                    new UserMenuSubCategory { Name = "Report", Type =  Enums.ColumnType.S.ToString(), Icon = "icon-share", CategoryId = _UserMenuCategory.Id, CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now }
                };

                foreach (UserMenuSubCategory s in _UserMenuSubCategory)
                {
                    await _context.AddAsync(s);
                }
                _context.SaveChanges();
            }

            //Menu
            if (!_context.UserMenus.Any())
            {
                var _Company = _context.Companies.OrderBy(a => a.Id).FirstOrDefault();
                var _UserMenuModule = _context.UserMenuModules.Where(w => w.Name.ToLower() == "Configuration".ToLower()).FirstOrDefault();
                var _UserMenuSubCategory = _context.UserMenuSubCategories.Where(w => w.Name.ToLower() == "Setup".ToLower()).FirstOrDefault();

                var _UserMenu = new UserMenu[] {
                    new UserMenu { Name = "Company", Alias = "Company", Type =  Enums.ColumnType.S.ToString(), ModuleId = _UserMenuModule.Id, SubCategoryId = _UserMenuSubCategory.Id, CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, View = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now },
                    new UserMenu { Name = "Branch", Alias = "Branch", Type =  Enums.ColumnType.S.ToString(), ModuleId = _UserMenuModule.Id, SubCategoryId = _UserMenuSubCategory.Id, CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, View = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now },
                    new UserMenu { Name = "User", Alias = "User", Type =  Enums.ColumnType.S.ToString(), ModuleId = _UserMenuModule.Id, SubCategoryId = _UserMenuSubCategory.Id, CompanyId = _Company.Id, Action = Enums.Operations.A.ToString (), Active = true, View = true, UserIdInsert = _UserId.Id, InsertDate = DateTime.Now }
                };

                foreach (UserMenu s in _UserMenu)
                {
                    await _context.AddAsync(s);
                }
                _context.SaveChanges();
            }

        }
    }
}