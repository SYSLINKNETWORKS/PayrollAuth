using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Bussiness {
    public class BUserRolePermission : AbsBusiness {
        private readonly DataContext _context;
        String _UserId = "";
        private readonly UserManager<ApplicationUser> _userManager;

        public BUserRolePermission (DataContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // public override async Task<ApiResponse> GetDataAsync (ClaimsPrincipal _User) {
        //     var ApiResponse = new ApiResponse ();
        //     try {

        //         var _Table = await (from g in _context.Roles join p in _context.UserRolePermissions on g.Id equals p.Roles_Id where p.Action != Enums.Operations.D.ToString () select g).Distinct ().ToListAsync ();

        //         if (_Table == null) {
        //             ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
        //             ApiResponse.message = "Record Not Found";
        //             return ApiResponse;
        //         }

        //         if (_Table.Count == 0) {
        //             ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString (); // "401";
        //             ApiResponse.message = "Record Not Found";
        //             return ApiResponse;
        //         }

        //         ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
        //         ApiResponse.data = _Table;
        //         return ApiResponse;

        //     } catch (Exception e) {

        //         string innerexp = "";
        //         if (e.InnerException != null) {
        //             innerexp = " Inner Error : " + e.InnerException.ToString ();
        //         }
        //         ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
        //         ApiResponse.message = e.Message.ToString () + innerexp;
        //         return ApiResponse;
        //     }
        // }
        public override async Task<ApiResponse> GetDataByIdAsync (Guid _Id, ClaimsPrincipal _User) {
            var ApiResponse = new ApiResponse ();
            try {

                var _Table = await _context.Roles.Where (a => a.Id == _Id.ToString ()).FirstOrDefaultAsync ();
                if (_Table == null) {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    ApiResponse.message = "Role not found";
                    return ApiResponse;
                }

                List<UserRolePermission> _TableRolePermission = (List<UserRolePermission>) await _context.UserRolePermissions.Where (x => x.Roles_Id == _Table.Id.ToString ()).ToListAsync ();
                List<MenuPerView> _MenuPerView = new List<MenuPerView> ();

                var _Menu = await _context.UserMenus.Include (s => s.userMenuSubCategory).Include (c => c.userMenuSubCategory.userMenuCategory).Include (m => m.userMenuModule).Where (x => x.Active == true && x.userMenuModule.Active == true && x.userMenuSubCategory.Active == true && x.userMenuSubCategory.userMenuCategory.Active == true && x.Action != Enums.Operations.D.ToString ()).ToListAsync ();
                foreach (var item in _Menu) {

                    var _Permission = _TableRolePermission.Where (x => x.Menu_Id == item.Id).FirstOrDefault ();
                    bool _View = false, _Insert = false, _Update = false, _Delete = false, _Print = false, _Check = false, _Approve = false;
                    if (_Permission != null) {
                        _View = _Permission.View_Permission;
                        _Insert = _Permission.Insert_Permission;
                        _Update = _Permission.Update_Permission;
                        _Delete = _Permission.Delete_Permission;
                        _Print = _Permission.Print_Permission;
                        _Check = _Permission.Check_Permission;
                        _Approve = _Permission.Approve_Permission;

                    }
                    _MenuPerView.Add (new MenuPerView { Module_Id = item.ModuleId, Module_Name = item.userMenuModule.Name, Category_Name = item.userMenuSubCategory.userMenuCategory.Name, SubCategory_Name =item.userMenuSubCategory.Name, Menu_Id = item.Id, Menu_Name = item.Name.Trim (), Menu_Alias = item.Alias.Trim (), Insert_Permission = _Insert, View_Permission = _View, Update_Permission = _Update, Delete_Permission = _Delete, Print_Permission = _Print, Check_Permission = _Check, Approve_Permission = _Approve });
                }
                var _ViewModel = new RoleViewByIdModel {
                    Id = _Table.Id,
                    Name = _Table.Name,
                    menuPerViews = _MenuPerView,
                };
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                ApiResponse.data = _ViewModel;
                return ApiResponse;

            } catch (Exception e) {

                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                ApiResponse.message = e.Message.ToString () + innerexp;
                return ApiResponse;
            }
        }
        // public override async Task<ApiResponse> AddAsync (object model, ClaimsPrincipal _User) {
        //     var ApiResponse = new ApiResponse ();
        //     try {
        //         _UserId = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.UserId.ToString ())?.Value.ToString ();
        //         var _model = (List<UserRolePermission>) model;

        //         bool gpMenuExists = _context.UserRolePermissions.Any (rec => rec.Roles_Id.Equals (_model[0].Roles_Id) && rec.Menu_Id.Equals (_model[0].Menu_Id) && rec.Action != Enums.Operations.D.ToString ());
        //         if (gpMenuExists) {
        //             ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString ();
        //             ApiResponse.message = "Menu Permisson Already Exist For Selected Role.";
        //             return ApiResponse;
        //         }

        //         foreach (var item in _model) {
        //             item.UserIdInsert = _UserId;
        //             item.InsertDate = DateTime.Now;
        //             item.Action = Enums.Operations.A.ToString ();
        //             item.Type = Enums.Operations.S.ToString ();
        //             await _context.UserRolePermissions.AddAsync (item);
        //         }

        //         _context.SaveChanges ();
        //         ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
        //         ApiResponse.message = "Record Saved";
        //         return ApiResponse;
        //     } catch (Exception e) {
        //         string innerexp = "";
        //         if (e.InnerException != null) {
        //             innerexp = " Inner Error : " + e.InnerException.ToString ();
        //         }
        //         ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
        //         ApiResponse.message = e.Message.ToString () + innerexp;
        //         return ApiResponse;
        //     }
        // }

        public override async Task<ApiResponse> AddAsync (object model, ClaimsPrincipal _User) {
            var ApiResponse = new ApiResponse ();
            try {
                _UserId = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.UserId.ToString ())?.Value.ToString ();
                var _model = (List<UserRolePermission>) model;

                var _modelDetails = _context.UserRolePermissions.Where (a => a.Roles_Id == _model[0].Roles_Id).ToList ();
                if (_modelDetails != null) {
                    foreach (var item in _modelDetails) {
                        _context.UserRolePermissions.Remove (item);
                    }
                    _context.SaveChanges();
                }


                foreach (var item in _model) {
                    item.UserIdUpdate = _UserId;
                    item.UpdateDate = DateTime.Now;
                    item.Action = Enums.Operations.E.ToString ();
                    item.Type = Enums.Operations.S.ToString ();
                    await _context.UserRolePermissions.AddAsync (item);
                }

                await _context.SaveChangesAsync ();

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                ApiResponse.message = "Record Save";
                return ApiResponse;
            } catch (Exception e) {
                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                ApiResponse.message = e.Message.ToString () + innerexp;
                return ApiResponse;
            }
        }
        // public override async Task<ApiResponse> DeleteAsync (Guid _Id, ClaimsPrincipal _User) {
        //     var ApiResponse = new ApiResponse ();
        //     try {
        //         _UserId = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.UserId.ToString ())?.Value.ToString ();

        //         var _Table = _context.UserRolePermissions.Where (a => a.Roles_Id == _Id.ToString () && a.Action != Enums.Operations.D.ToString ()).ToList ();
        //         if (_Table == null || _Table.Count == 0) {
        //             ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
        //             ApiResponse.message = "Record not found ";
        //             return ApiResponse;
        //         }

        //         // var users = _context.Users.Where (a => a.RolesId == _Table[0].Roles_Id).ToList ();

        //         // if (users != null && users.Count > 0) {
        //         //     ApiResponse.statusCode = StatusCodes.Status403Forbidden.ToString ();
        //         //     ApiResponse.message = "Unable to delete, first remove users of this group";
        //         //     return ApiResponse;
        //         // }

        //         foreach (var item in _Table) {
        //             item.Action = Enums.Operations.D.ToString ();
        //             item.UserIdDelete = _UserId;
        //             item.DeleteDate = DateTime.Now;
        //         }

        //         // if (users != null && users.Count > 0)
        //         // {
        //         //     var GPCliams = await General.GetGroupClaims(_context, _Table.GP_Id);

        //         //     if (GPCliams.Count > 0)
        //         //     {
        //         //         foreach (var user in users)
        //         //         {
        //         //             foreach (var gpclaim in GPCliams)
        //         //             {
        //         //                 // gets the claims of the user
        //         //                 var existingUserClaims = _context.UserClaims.Where(a => a.UserId == user.Id && a.ClaimType.Trim().ToLower() == gpclaim.ClaimType.Trim().ToLower()).FirstOrDefault();
        //         //                 if (existingUserClaims != null)
        //         //                 {
        //         //                     _context.UserClaims.Remove(existingUserClaims);
        //         //                 }
        //         //             }
        //         //         }
        //         //     }
        //         // }

        //         await _context.SaveChangesAsync ();
        //         ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
        //         ApiResponse.message = "Record Deleted";
        //         return ApiResponse;
        //     } catch (Exception e) {
        //         string innerexp = "";
        //         if (e.InnerException != null) {
        //             innerexp = " Inner Error : " + e.InnerException.ToString ();
        //         }
        //         ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
        //         ApiResponse.message = e.Message.ToString () + innerexp;
        //         return ApiResponse;
        //     }
        // }
    }
}