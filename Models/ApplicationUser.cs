using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TWP_API_Auth.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }


        [Required]
        public Guid BranchId { get; set; }
        // Navigation Property
        [ForeignKey("BranchId")]
        public Branch Branches { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }

        // [Required]
        // [NotMapped]
        // public string RoleName { get; set; }

        public Guid? EmployeeId { get; set; }
        // // Navigation Property
        // [ForeignKey("EmployeeId")]
        // public Employee employees { get; set; }

        [Required]
        public bool AllBranchCheck { get; set; }

        // [Required]
        // public DateTime PermissionDateFrom { get; set; }

        // [Required]
        // public DateTime PermissionDateTo { get; set; }
        [Required]
        public int BackLog { get; set; }
        [Required]
        public bool Ck_RequiredAttandance { get; set; }
        [Required]
        public bool Ck_OnlineAttandance { get; set; }

        [Required]
        [StringLength(1)]
        public string Type { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        [StringLength(1)]
        public string Action { get; set; }

        [StringLength(450)]
        public string UserIdInsert { get; set; }

        [Required]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        [StringLength(450)]
        public string UserIdUpdate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [StringLength(450)]
        public string UserIdDelete { get; set; }

        [Required]
        public DateTime DeleteDate { get; set; } = DateTime.Now;
       
    }
}