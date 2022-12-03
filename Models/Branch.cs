using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWP_API_Auth.Models;

namespace TWP_API_Auth.Models
{
    [Table("Branch")]
    public partial class Branch
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Mobile { get; set; }

        [StringLength(250)]
        public string Fax { get; set; }


        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(250)]
        public string Web { get; set; }

        public bool HeadOffice { get; set; } = false;

        public string LogoHeader { get; set; }
        public string LogoFooter { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        // Navigation Property
        [ForeignKey("CompanyId")]
        public Company Companies { get; set; }

        [Required]
        [StringLength(1)]
        public string Type { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        [StringLength(1)]
        public string Action { get; set; }
        public string UserNameInsert { get; set; }
        // Navigation Property

        [Required]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        public string UserNameUpdate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public string UserNameDelete { get; set; }

        [Required]
        public DateTime DeleteDate { get; set; } = DateTime.Now;

    }
}