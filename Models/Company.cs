using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TWP_API_Auth.Models;

namespace TWP_API_Auth.Models
{
    [Table("Company")]
    public partial class Company
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }
        public string NTN { get; set; }

        public string STN { get; set; }



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