using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace TWP_API_Auth.Helpers {
    public static class General {
        public static async Task<List<UserClaim>> GetGroupClaims (DataContext _context, String Roles_Id) {
            List<UserClaim> GPCliams = new List<UserClaim> ();
            var GroupPermission = await _context.UserRolePermissions.Where (a => a.Roles_Id == Roles_Id && a.Action != "D").ToListAsync ();
            foreach (var item in GroupPermission) {
                var AuthClaims = await _context.AuthClaims.Where (a => a.Menu_Id == item.Menu_Id).ToListAsync ();
                if (AuthClaims.Count > 0) {
                    foreach (var claim in AuthClaims) {
                        UserClaim gpclaim = new UserClaim ();
                        if (claim.ClaimType.ToString ().Contains ("View")) {
                            if (item.View_Permission) {
                                gpclaim.ClaimType = claim.ClaimType;
                                GPCliams.Add (gpclaim);
                            }
                        } else if (claim.ClaimType.ToString ().Contains ("Insert")) {
                            if (item.Insert_Permission) {
                                gpclaim.ClaimType = claim.ClaimType;
                                GPCliams.Add (gpclaim);
                            }
                        } else if (claim.ClaimType.ToString ().Contains ("Update")) {
                            if (item.Update_Permission) {
                                gpclaim.ClaimType = claim.ClaimType;
                                GPCliams.Add (gpclaim);
                            }
                        } else if (claim.ClaimType.ToString ().Contains ("Delete")) {
                            if (item.Delete_Permission) {
                                gpclaim.ClaimType = claim.ClaimType;
                                GPCliams.Add (gpclaim);
                            }
                        }
                    }
                }
            }

            return GPCliams;
        }
    }
}