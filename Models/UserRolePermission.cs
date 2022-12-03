using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWP_API_Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace TWP_API_Auth.Models
{
    [Table("UserRolePermission")]
    public partial class UserRolePermission
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public bool View_Permission { get; set; }=false;

        [Required]
        public bool Insert_Permission { get; set; }=false;

        [Required]
        public bool Update_Permission { get; set; }=false;

        [Required]
        public bool Delete_Permission { get; set; }=false;

        [Required]
        public bool Print_Permission { get; set; } =false;

        [Required]
        public bool Check_Permission { get; set; }=false;
        [Required]
        public bool Approve_Permission { get; set; }=false;

        [Required]
        [StringLength(450)]
        public string Roles_Id { get; set; }

        // Navigation Property
        [ForeignKey("Roles_Id")]
        public IdentityRole IdentityRoles { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }

        // Navigation Property
        [ForeignKey("Menu_Id")]
        public UserMenu UserMenu { get; set; }

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
        // Navigation Property
        [ForeignKey("UserIdInsert")]
        public ApplicationUser applicationUserInsert { get; set; }

        [Required]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        [StringLength(450)]
        public string UserIdUpdate { get; set; }
        // Navigation Property
        [ForeignKey("UserIdUpdate")]
        public ApplicationUser applicationUserUpdate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [StringLength(450)]
        public string UserIdDelete { get; set; }

        // Navigation Property
        [ForeignKey("UserIdDelete")]
        public ApplicationUser applicationUserDelete { get; set; }
        [Required]
        public DateTime DeleteDate { get; set; } = DateTime.Now;

    }
}