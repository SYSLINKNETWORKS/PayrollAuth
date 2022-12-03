using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWP_API_Auth.ViewModels
{
    public class RoleUsersViewModel
    {
        [Display(Name = "User Id")]
        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
