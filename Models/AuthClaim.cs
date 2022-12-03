using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWP_API_Auth.Models;

namespace TWP_API_Auth.Models {
    [Table ("AuthClaim")]
    public partial class AuthClaim {
        [Key]
        public Guid Id { get; set; } = new Guid ();
        [Required]
        public string ClaimType { get; set; }

        [Required]
        public string ClaimValue { get; set; }

        [Required]
        public Guid Menu_Id { get; set; }
        // Navigation Property
        [ForeignKey ("Menu_Id")]
        public UserMenu UserMenu { get; set; }

        [Required]
        [StringLength (1)]
        public string Type { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        [StringLength (1)]
        public string Action { get; set; }

        [StringLength (450)]
        public string UserIdInsert { get; set; }
        // Navigation Property
        [ForeignKey ("UserIdInsert")]
        public ApplicationUser applicationUserInsert { get; set; }

        [Required]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        [StringLength (450)]
        public string UserIdUpdate { get; set; }
        // Navigation Property
        [ForeignKey ("UserIdUpdate")]
        public ApplicationUser applicationUserUpdate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        [StringLength (450)]
        public string UserIdDelete { get; set; }

        [Required]
        public DateTime DeleteDate { get; set; } = DateTime.Now;
        // Navigation Property
        [ForeignKey ("UserIdDelete")]
        public ApplicationUser applicationUserDelete { get; set; }

    }
}