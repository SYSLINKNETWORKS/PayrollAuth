using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TWP_API_Auth.Models {
    [Table ("UserMenuModule")]
    public partial class UserMenuModule {
        [Key]
        public Guid Id { get; set; } = new Guid ();

        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        [StringLength (1)]
        public string Type { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        // Navigation Property
        [ForeignKey ("CompanyId")]
        public Company Companies { get; set; }

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
        // Navigation Property
        [ForeignKey ("UserIdDelete")]
        public ApplicationUser applicationUserDelete { get; set; }

        [Required]
        public DateTime DeleteDate { get; set; } = DateTime.Now;

    }
}