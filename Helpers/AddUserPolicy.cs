using TWP_API_Auth.App_Data;
using System.Linq;
using TWP_API_Auth.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TWP_API_Auth.Helpers {
    public class AddUserPolicy {
        DataContext _context;
        public AddUserPolicy (DataContext contextDb) {

            _context = contextDb;
        }

        public async void AddPolicy (IServiceCollection services) {
            var _Menu = await _context.UserMenus.ToListAsync ();
            foreach (var _MenuList in _Menu) {
                services.AddAuthorization (options => {
                    options.AddPolicy (_MenuList.Name + Enums.Policies.View, policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim (_MenuList.Id.ToString (), Enums.Policies.View.ToString ())));
                    options.AddPolicy (_MenuList.Name + Enums.Policies.Insert, policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim (_MenuList.Id.ToString (), Enums.Policies.Insert.ToString ())));
                    options.AddPolicy (_MenuList.Name + Enums.Policies.Update, policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim (_MenuList.Id.ToString (), Enums.Policies.Update.ToString ())));
                    options.AddPolicy (_MenuList.Name + Enums.Policies.Delete, policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim (_MenuList.Id.ToString (), Enums.Policies.Delete.ToString ())));
                    options.AddPolicy (_MenuList.Name + Enums.Policies.ViewById, policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim (_MenuList.Id.ToString (), Enums.Policies.Update.ToString ()) || context.User.HasClaim ("08d8dbc0-e56b-44fb-8af4-cc0f596884d2", Enums.Policies.Delete.ToString ())));

                });
            }

            // //Company
            // options.AddPolicy ("CompanyView", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e56b-44fb-8af4-cc0f596884d2", Enums.Policies.View.ToString ())));
            // options.AddPolicy ("CompanyInsert", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e56b-44fb-8af4-cc0f596884d2", Enums.Policies.Insert.ToString ())));
            // options.AddPolicy ("CompanyUpdate", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e56b-44fb-8af4-cc0f596884d2", Enums.Policies.Update.ToString ())));
            // options.AddPolicy ("CompanyDelete", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e56b-44fb-8af4-cc0f596884d2", Enums.Policies.Delete.ToString ())));
            // options.AddPolicy ("CompanyById", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e56b-44fb-8af4-cc0f596884d2", Enums.Policies.Update.ToString ()) || context.User.HasClaim ("08d8dbc0-e56b-44fb-8af4-cc0f596884d2", Enums.Policies.Delete.ToString ())));

            // //Branch
            // options.AddPolicy ("BranchView", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e922-4c89-8789-c09c6aa2e3a1", Enums.Policies.View.ToString ())));
            // options.AddPolicy ("BranchInsert", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e922-4c89-8789-c09c6aa2e3a1", Enums.Policies.Insert.ToString ())));
            // options.AddPolicy ("BranchUpdate", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e922-4c89-8789-c09c6aa2e3a1", Enums.Policies.Update.ToString ())));
            // options.AddPolicy ("BranchDelete", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e922-4c89-8789-c09c6aa2e3a1", Enums.Policies.Delete.ToString ())));
            // options.AddPolicy ("BranchById", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-e922-4c89-8789-c09c6aa2e3a1", Enums.Policies.Update.ToString ()) || context.User.HasClaim ("08d8dbc0-e922-4c89-8789-c09c6aa2e3a1", Enums.Policies.Delete.ToString ())));

            // //User
            // options.AddPolicy ("UserView", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-c9bb-4b48-8054-bf1310aee2d7", Enums.Policies.View.ToString ())));
            // options.AddPolicy ("UserInsert", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-c9bb-4b48-8054-bf1310aee2d7", Enums.Policies.Insert.ToString ())));
            // options.AddPolicy ("UserUpdate", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-c9bb-4b48-8054-bf1310aee2d7", Enums.Policies.Update.ToString ())));
            // options.AddPolicy ("UserDelete", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-c9bb-4b48-8054-bf1310aee2d7", Enums.Policies.Delete.ToString ())));
            // options.AddPolicy ("UserById", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-c9bb-4b48-8054-bf1310aee2d7", Enums.Policies.Update.ToString ()) || context.User.HasClaim ("08d8dbc0-c9bb-4b48-8054-bf1310aee2d7", Enums.Policies.Delete.ToString ())));

            // // Menu
            // options.AddPolicy ("MenuView", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-dce8-409d-80e1-977bc94d5ef5", Enums.Policies.View.ToString ())));
            // options.AddPolicy ("MenuInsert", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-dce8-409d-80e1-977bc94d5ef5", Enums.Policies.Insert.ToString ())));
            // options.AddPolicy ("MenuUpdate", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-dce8-409d-80e1-977bc94d5ef5", Enums.Policies.Update.ToString ())));
            // options.AddPolicy ("MenuDelete", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-dce8-409d-80e1-977bc94d5ef5", Enums.Policies.Delete.ToString ())));
            // options.AddPolicy ("MenuById", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-dce8-409d-80e1-977bc94d5ef5", Enums.Policies.Update.ToString ()) || context.User.HasClaim ("08d8dbc0-dce8-409d-80e1-977bc94d5ef5", Enums.Policies.Delete.ToString ())));

            // //Role
            // options.AddPolicy ("RoleView", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-d707-4695-8a7e-98838266025c", Enums.Policies.View.ToString ())));
            // options.AddPolicy ("RoleInsert", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-d707-4695-8a7e-98838266025c", Enums.Policies.Insert.ToString ())));
            // options.AddPolicy ("RoleUpdate", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-d707-4695-8a7e-98838266025c", Enums.Policies.Update.ToString ())));
            // options.AddPolicy ("RoleDelete", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-d707-4695-8a7e-98838266025c", Enums.Policies.Delete.ToString ())));
            // options.AddPolicy ("RoleById", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc0-d707-4695-8a7e-98838266025c", Enums.Policies.Update.ToString ()) || context.User.HasClaim ("08d8dbc0-d707-4695-8a7e-98838266025c", Enums.Policies.Delete.ToString ())));

            // //Role Permission
            // options.AddPolicy ("RolePermissionView", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc1-227e-43e9-8b98-b2f14e2dde63", Enums.Policies.View.ToString ())));
            // options.AddPolicy ("RolePermissionInsert", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc1-227e-43e9-8b98-b2f14e2dde63", Enums.Policies.Insert.ToString ())));
            // options.AddPolicy ("RolePermissionUpdate", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc1-227e-43e9-8b98-b2f14e2dde63", Enums.Policies.Update.ToString ())));
            // options.AddPolicy ("RolePermissionDelete", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc1-227e-43e9-8b98-b2f14e2dde63", Enums.Policies.Delete.ToString ())));
            // options.AddPolicy ("RolePermissionById", policy => policy.RequireAssertion (context => context.User.HasClaim (Enums.Roles.Role.ToString (), Enums.Roles.SuperAdmin.ToString ()) || context.User.HasClaim ("08d8dbc1-227e-43e9-8b98-b2f14e2dde63", Enums.Policies.Update.ToString ()) || context.User.HasClaim ("08d8dbc1-227e-43e9-8b98-b2f14e2dde63", Enums.Policies.Delete.ToString ())));

            //});

        }
    }
}