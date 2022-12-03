using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TWP_API_Auth.Bussiness;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Models;
using TWP_API_Auth.Services;

namespace TWP_API_Auth.Generic
{
    public static class Builder
    {
        public static AbsBusiness MakeBusinessClass(Enums.ClassName ClassName, App_Data.DataContext _context)
        {
            switch (ClassName)
            {

                case Enums.ClassName.UserMenu:
                    {
                        return new BUserMenu(_context);
                    }
                case Enums.ClassName.UserMenuModule:
                    {
                        return new BUserMenuModule(_context);
                    }
                case Enums.ClassName.UserMenuCategory:
                    {
                        return new BUserMenuCategory(_context);
                    }
                case Enums.ClassName.UserMenuSubCategory:
                    {
                        return new BUserMenuSubCategory(_context);
                    }

                case Enums.ClassName.AuthClaim:
                    {
                        return new BAuthClaim(_context);
                    }

                case Enums.ClassName.Company:
                    {
                        return new BCompany(_context);
                    }
                case Enums.ClassName.Branch:
                    {
                        return new BBranch(_context);
                    }
                case Enums.ClassName.FinancialYear:
                    {
                        return new BFinancialYear(_context);
                    }
                default:
                    return null;

            }

        }
        public static AbsBusiness MakeBusinessClass(Enums.ClassName ClassName, App_Data.DataContext _context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManger)
        {
            switch (ClassName)
            {
                case Enums.ClassName.Role:
                    {
                        return new BRole(roleManager);
                    }

                case Enums.ClassName.UserClaim:
                    {
                        return new BUserClaim(userManger);
                    }

                case Enums.ClassName.User:
                    {
                        return new BUsers(_context, userManger, roleManager);
                    }
                case Enums.ClassName.UserRole:
                    {
                        return new BUserRole(userManger, roleManager);
                    }
                case Enums.ClassName.RolePermission:
                    {
                        return new BUserRolePermission(_context, userManger);
                    }

                default:
                    return null;
            }
        }

    }
}