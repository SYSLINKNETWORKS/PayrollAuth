using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TWP_API_Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace TWP_API_Auth.ViewModels
{
    public class UserRolesViewModel
    {

        public string UserId { get; set; }
        public List<IdentityRole> Roles { get; set; }

    }
}
